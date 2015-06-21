namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using System;
    using System.Collections.Generic;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class JoinGenerator
    {
        private readonly ObjectStringService objectStringService;

        public JoinGenerator(ObjectStringService objectStringService)
        {
            this.objectStringService = objectStringService;
        }

        public void GenerateJoin(List<MappingField> farEndFields, List<MappingField> nearEndFields, IStringGenerator stringGenerator, bool semicolon)
        {
            var farend = GetFields(farEndFields, nearEndFields);
            var nearend = GetFields(nearEndFields, farEndFields);

            var sourceJoin = ".Join(sourceQuery,";
            var firstArg = "x => " + objectStringService.CreateObjectString(farend, "x") + ",";
            var secondArg = "x => " + objectStringService.CreateObjectString(nearend, "x") + ",";
            var ending = "(x, y) => x)" + (semicolon ? ";" : string.Empty);

            if (farEndFields.Count == 1)
            {
                stringGenerator.AppendLine(sourceJoin + " " + firstArg + " " + secondArg + " " + ending);
            }
            else
            {
                stringGenerator.AppendLine(sourceJoin);
                stringGenerator.AppendLine(firstArg);
                stringGenerator.AppendLine(secondArg);
                stringGenerator.AppendLine(ending);
            }
        }

        private List<string> GetFields(List<MappingField> these, List<MappingField> other)
        {
            var output = new List<string>(these.Count);
            for (int i = 0; i < these.Count; i++)
            {
                output.Add(these[i].Name + (these[i].DbField.IsNullable && !other[i].DbField.IsNullable ? ".Value" : string.Empty));
            }

            return output;
        }
    }
}
