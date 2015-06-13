namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class CreateGenerator : MethodGenerator
    {
        public override void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + " Create(IDataReader reader, ILoadService loadService)");
        }

        public override void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var entity = new " + model.Name + "(source)");
            stringGenerator.Braces(() => GenerateFields(model, parent, stringGenerator), true);
            stringGenerator.AppendLine("extension.ExtendCreate(entity, reader);");
            stringGenerator.AppendLine("return entity;");
        }

        private void GenerateFields(Model model, Model parent, IStringGenerator stringGenerator)
        {
            foreach (var field in model.MappingFields)
            {
                stringGenerator.AppendLine(field.Name + " = reader." + field.Name + ",");
            }
        }
    }
}
