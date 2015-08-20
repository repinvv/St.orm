namespace StormGenerator.Generation.RepositoryGeneration
{
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryConstructorGenerator
    {
        public void GenerateConstructor(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"private IDalRepositoryExtension<{model.Name}> extension;");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"public {model.Name}{GenerationConstants.ModelGeneration.RepositorySuffix}()");
            stringGenerator.Braces($"extension = new EmptyRepositoryExtension<{model.Name}>();");
        }
    }
}
