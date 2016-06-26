namespace StormGenerator.Generation.Generators.GeneratorHelpers
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models;
    using StormGenerator.Models.GenModels;
    using StormGenerator.Settings;

    internal static class ModelHelpers
    {
        public static string GetNamespace(this EntityModel model, GenOptions options)
        {
            if (model.Model.NamespaceSuffix == null)
            {
                return options.OutputNamespace;
            }

            return options.OutputNamespace + "." + model.Model.NamespaceSuffix;
        }

        public static IEnumerable<string> GetUsings(this Model model)
        {
            // var fixedNamespaces = new[] { typeof(StormAccess).Namespace };
            var namespaces = model.Fields
                                  .Select(x => x.Column.CsType.Namespace)
                                  .Distinct()
                                  .OrderBy(x => x);
            return namespaces;
        }

        public static IEnumerable<string> GetFieldSelectLines(this Model model, string prefix)
        {
            var groups = model.Fields.SplitInGroupsBy(8).ToList();
            foreach (var g in groups)
            {
                var join = string.Join(", ", g.Select(x => prefix + x.Column.Name));
                yield return g == groups.Last() ? @join : @join + ",";
            }
        }

        public static string GetTuple(this Model model)
        {
            return $"Tuple<{string.Join(", ", model.KeyFields.Select(x => x.Column.CsTypeName + "[]"))}>";
        }

        public static IEnumerable<string> GetKeyEqualityLines(this Model model, 
            string leftPrefix,
            string rightPrefix,
            string closeLastLine)
        {
            var groups = model.KeyFields.SplitInGroupsBy(8).ToList();
            foreach (var g in groups)
            {
                var join = string.Join(" AND ", g.Select(x => $"{leftPrefix}{x.Column.Name} = {rightPrefix}{x.Column.Name}"));
                yield return g != groups.Last() ? join + " AND" : join + closeLastLine;
            }
        }
    }
}
