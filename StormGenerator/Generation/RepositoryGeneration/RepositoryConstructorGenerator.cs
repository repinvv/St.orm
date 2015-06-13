namespace StormGenerator.Generation.RepositoryGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryConstructorGenerator
    {
        public void GenerateConstructor(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private readonly IDalRepositoryExtension<" + model.Name + "> extension;");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public CalculationDalRepository(IDalRepositoryExtension<" + model.Name + "> extension)");
            stringGenerator.Braces(() => stringGenerator.AppendLine("this.extension = extension;"));
        }
    }
}
