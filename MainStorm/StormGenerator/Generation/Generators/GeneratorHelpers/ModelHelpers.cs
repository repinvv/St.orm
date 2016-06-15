﻿namespace StormGenerator.Generation.Generators.GeneratorHelpers
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
            var namespaces = model.Fields
                                  .Select(x => x.Column.CsType.Namespace)
                                  .Distinct();
            return namespaces;
        }

        public static string TypeDefault(this Field field)
        {
            return field.Column.CsType == typeof(string) 
                ? "string.Empty" 
                : $"default({field.Column.CsTypeName})";
        }
    }
}
