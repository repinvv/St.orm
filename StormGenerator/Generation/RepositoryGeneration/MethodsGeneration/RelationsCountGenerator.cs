namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RelationsCountGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public int RelationsCount()");
        }

        public void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("return extension.RelationsCount() ?? " + model.RelationFields.Count + ";");
        }
    }
}
