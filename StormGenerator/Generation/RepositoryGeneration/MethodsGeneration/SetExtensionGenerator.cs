namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class SetExtensionGenerator : MethodGenerator
    {
        public override void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void SetExtension(IDalRepositoryExtension<" + model.Name + "> extension)");
        }

        public override void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("this.extension = extension;");
        }
    }
}
