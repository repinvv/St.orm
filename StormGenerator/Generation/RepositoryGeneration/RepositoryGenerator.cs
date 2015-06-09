namespace StormGenerator.Generation.RepositoryGeneration
{
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryGenerator
    {
        private readonly FileGenerator fileGenerator;

        public RepositoryGenerator(FileGenerator fileGenerator)
        {
            this.fileGenerator = fileGenerator;
        }

        public GeneratedFile GenerateRepository(Model model, Options options)
        {
            var name = model.Name + GenerationConstants.ModelGeneration.RepositorySuffix;
            return fileGenerator.GenerateFile(name, options, stringGenerator => GenerateDefinition(model, stringGenerator));
        }

        private void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("internal class " + model.Name + GenerationConstants.ModelGeneration.RepositorySuffix);
            stringGenerator.Braces(() => { });
        }
    }
}
