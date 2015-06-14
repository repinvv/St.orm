namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Generation.Common;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class CreateGenerator : IMethodGenerator
    {
        private readonly MaterializerLineGenerator materializerLineGenerator;
        private readonly FieldTypeService fieldTypeService;

        public CreateGenerator(MaterializerLineGenerator materializerLineGenerator, FieldTypeService fieldTypeService)
        {
            this.materializerLineGenerator = materializerLineGenerator;
            this.fieldTypeService = fieldTypeService;
        }

        public void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + " Create(IDataReader reader, ILoadService loadService)");
        }

        public void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var entity = new " + model.Name + "(loadService)");
            stringGenerator.Braces(() => GenerateFields(model, parent, stringGenerator), true);
            stringGenerator.AppendLine("extension.ExtendCreate(entity, reader);");
            stringGenerator.AppendLine("return entity;");
        }

        private void GenerateFields(Model model, Model parent, IStringGenerator stringGenerator)
        {
            foreach (var field in model.MappingFields)
            {
                var type = fieldTypeService.GetFieldType(field.DbField.Type, field.DbField.IsNullable);
                stringGenerator.AppendLine(field.Name + " = " + materializerLineGenerator.GenerateMaterializerLine("reader", type, field.DbField.Index) + ",");
            }
        }
    }
}
