namespace StormGenerator.Generation.ModelGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.Common;
    using StormGenerator.Generation.ModelGeneration.LazyGeneration;
    using StormGenerator.Generation.ModelGeneration.ModelPartsGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ModelFieldsGenerator
    {
        private readonly NameNormalizer nameNormalizer;
        private readonly FieldUtility fieldUtility;
        private readonly LazyGetGenerator lazyGetGenerator;
        private readonly TypeService typeService;

        public ModelFieldsGenerator(NameNormalizer nameNormalizer,
            FieldUtility fieldUtility,
            LazyGetGenerator lazyGetGenerator,
            TypeService typeService)
        {
            this.nameNormalizer = nameNormalizer;
            this.fieldUtility = fieldUtility;
            this.lazyGetGenerator = lazyGetGenerator;
            this.typeService = typeService;
        }

        public void GenerateProperties(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            var names = nameNormalizer.NormalizeNames(model.MappingFields.ActiveSelect(x => x.Name).ToList());
            var fields = model.MappingFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                field.Name = names[index];
                stringGenerator.AppendLine($"public {typeService.GetTypeName(field.Type)} {field.Name} {{ get;set; }}");
                stringGenerator.AppendLine();
            }
        }

        public void GeneratePrivateFields(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            partGenerator.GeneratePrivateFields(model, stringGenerator);
            var fields = model.RelationFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                stringGenerator.AppendLine($"private {fieldUtility.GetRelationFieldType(field)} field{index};");
            }
        }

        public void GenerateLazyProperties(Model model, IStringGenerator stringGenerator)
        {
            var relationFields = model.RelationFields.Active();
            var names = nameNormalizer.NormalizeNames(relationFields.Select(x => x.Name).ToList());
            for (int index = 0; index < relationFields.Count; index++)
            {
                var field = relationFields[index];
                field.Name = names[index];

                stringGenerator.AppendLine($"public virtual {fieldUtility.GetRelationFieldType(field)} {field.Name}");
                stringGenerator
                    .Braces(() => stringGenerator
                                .Region("implementation", () => GenerateLazyProperty(field, index, model, stringGenerator)));
                stringGenerator.AppendLine();
            }
        }

        private void GenerateLazyProperty(RelationField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("get");
            stringGenerator.Braces(() => lazyGetGenerator.GenerateLazyGet(field, index, model, stringGenerator));
            stringGenerator.AppendLine("set");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine($"field{index} = value;");
                stringGenerator.AppendLine($"populated[{index}] = true;");
            });
        }
    }
}
