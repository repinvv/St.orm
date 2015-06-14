namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class CloneGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + " Clone(" + model.Name + " source)");
        }

        public void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var clone = new " + model.Name + "(source)");
            stringGenerator.Braces(() => GenerateFields(model, parent, stringGenerator), true);
            stringGenerator.AppendLine("extension.ExtendClone(clone, source);");
            stringGenerator.AppendLine("return clone;");
        }

        private void GenerateFields(Model model, Model parent, IStringGenerator stringGenerator)
        {
            foreach (var field in model.MappingFields)
            {
                stringGenerator.AppendLine(field.Name + " = source." + field.Name + ",");
            }
        }
    }
}
