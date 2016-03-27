namespace StormGenerator.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;

    internal class UsingsGenerator
    {
        public void GenerateUsings(IStringGenerator stringGenerator, IEnumerable<string> usings)
        {
            var sortedUsings = usings.Distinct()
                                     .OrderBy(x => x.StartsWith("System") ? 0 : 1)
                                     .ThenBy(x => x);

            foreach (var @using in sortedUsings)
            {
                stringGenerator.AppendLine("using " + @using + ";");
            }
        }

        public void GenerateTypeAliases(IStringGenerator stringGenerator, IEnumerable<Type> types)
        {
            types = types.Distinct()
                         .OrderBy(x => x.Name);
            foreach (var fieldType in types)
            {
                stringGenerator.AppendLine($"using {fieldType.Name} = {fieldType.FullName};");
            }
        }
    }
}
