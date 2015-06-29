namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class JoinGenerator
    {
        private readonly ObjectStringService objectStringService;
        private readonly TypeService typeService;

        public JoinGenerator(ObjectStringService objectStringService, TypeService typeService)
        {
            this.objectStringService = objectStringService;
            this.typeService = typeService;
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
                stringGenerator.PushIndent();
                stringGenerator.AppendLine(firstArg);
                stringGenerator.AppendLine(secondArg);
                stringGenerator.AppendLine(ending);
                stringGenerator.PopIndent();
            }
        }

        private List<string> GetFields(List<MappingField> these, List<MappingField> other, bool defaultValue = false)
        {
            var value = defaultValue ? ".GetValueOrDefault()" : ".Value";
            var output = new List<string>(these.Count);
            for (int i = 0; i < these.Count; i++)
            {
                output.Add(these[i].Name + (these[i].DbField.IsNullable && !other[i].DbField.IsNullable && these.Count > 1 ? value : string.Empty));
            }

            return output;
        }

        public void GenerateGetItems(Model fieldModel, int index, List<MappingField> farEndFields, List<MappingField> nearEndFields, IStringGenerator stringGenerator, bool semicolon, bool isList)
        {
            var farend = GetFields(farEndFields, nearEndFields, true);
            var nearend = GetFields(nearEndFields, farEndFields, true);
            var keyType = farEndFields.Count == 1 ? typeService.GetTypeName(farEndFields[0].Type) : "object";
            var start = isList
                ? "var items = loadService.GetList<"
                : "var item = loadService.GetSingle<";
            var first = start + fieldModel.Name + ", " + fieldModel.Parent.Name +
                        ", " + keyType + ">(" + index + ",";
            var second = "query,";
            var third = "x => " + objectStringService.CreateObjectString(farend, "x") + ",";
            var fourth = objectStringService.CreateObjectString(nearend) + ")" + (semicolon ? ";" : string.Empty);
            if (farEndFields.Count == 1)
            {
                stringGenerator.AppendLine(first + " " + second + " " + third + " " + fourth);
            }
            else
            {
                stringGenerator.AppendLine(first);
                stringGenerator.PushIndent();
                stringGenerator.AppendLine(second);
                stringGenerator.AppendLine(third);
                stringGenerator.AppendLine(fourth);
                stringGenerator.PopIndent();
            }
        }
    }
}
