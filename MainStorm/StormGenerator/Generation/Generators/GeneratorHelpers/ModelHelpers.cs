﻿namespace StormGenerator.Generation.Generators.GeneratorHelpers
{
    using System;
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

        public static IEnumerable<string> GetKeyEqualityLines(this Model model,
            string leftPrefix,
            string rightPrefix,
            string closeLastLine)
        {
            var fields = model.KeyFields.Any() ? model.KeyFields : model.Fields;
            var groups = fields.SplitInGroupsBy(8)
                               .ToList();
            foreach (var g in groups)
            {
                var join = string.Join(" AND ", g.Select(x => $"{leftPrefix}{x.Column.Name} = {rightPrefix}{x.Column.Name}"));
                yield return g != groups.Last() ? join + " AND" : join + closeLastLine;
            }
        }

        public static string JoinKeyNames(this Model model)
        {
            int i = 0;
            return string.Join(", ", model.KeyFields.Select(x => "key" + i++));
        }

        public static string JoinKeys(this Model model)
        {
            int i = 0;
            return string.Join(", ", model.KeyFields.Select(x => x.Column.CsTypeName + "[] key" + i++));
        }

        public static List<Field> ValueFields(this Model model)
        {
            return model.Fields.Except(model.KeyFields).ToList();
        }
        public static List<Field> ValueFieldsThenKeys(this Model model)
        {
            return model.Fields.Except(model.KeyFields).Concat(model.KeyFields).ToList();
        }

        public static bool NeedsOutput(this Model model)
        {
            return model.KeyFields.Count == 1 &&
                   (model.KeyFields[0].Column.IsIdentity || model.KeyFields[0].Column.Sequence != null);
        }

        public static int MaxAmountForRegularGroupedInsert(this Model model)
        {
            return Math.Max((369 - 33 * model.Fields.Count) / 6, 10);
        }
        public static int MaxAmountForSequenceGroupedInsert(this Model model)
        {
            return Math.Max((361 - 27 * model.Fields.Count) / 7, 10);
        }

        public static int MaxAmountForGroupedUpdate(this Model model)
        {
            return Math.Max((632 - 48 * model.Fields.Count) / 5, 10);
        }
    }
}
