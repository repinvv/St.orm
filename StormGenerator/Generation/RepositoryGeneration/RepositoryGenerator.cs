namespace StormGenerator.Generation.RepositoryGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.ModelGeneration;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryGenerator
    {
        private readonly FileGenerator fileGenerator;
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly RepositoryMethodsGenerator repositoryMethodsGenerator;
        private readonly RepositoryConstructorGenerator repositoryConstructorGenerator;

        public RepositoryGenerator(FileGenerator fileGenerator,
            UsingsGenerator usingsGenerator, 
            FieldUtility fieldUtility,
            RepositoryMethodsGenerator repositoryMethodsGenerator, 
            RepositoryConstructorGenerator repositoryConstructorGenerator)
        {
            this.fileGenerator = fileGenerator;
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.repositoryMethodsGenerator = repositoryMethodsGenerator;
            this.repositoryConstructorGenerator = repositoryConstructorGenerator;
        }

        public GeneratedFile GenerateRepository(Model model, Model parent, Options options)
        {
            var name = model.Name + GenerationConstants.ModelGeneration.RepositorySuffix;
            return fileGenerator.GenerateFile(name, options, stringGenerator => GenerateDefinition(model, parent, stringGenerator));
        }

        private void GenerateDefinition(Model model, Model parent, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Active().Select(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.RepositoryUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("internal class " + model.Name + GenerationConstants.ModelGeneration.RepositorySuffix
                                       + " : IDalRepository<" + model.Name + ", " + parent.Name + ">");
            stringGenerator.Braces(() => GenerateRepositoryContent(model, parent, stringGenerator));
        }

        private void GenerateRepositoryContent(Model model, Model parent, IStringGenerator stringGenerator)
        {
            repositoryConstructorGenerator.GenerateConstructor(model, parent, stringGenerator);
            repositoryMethodsGenerator.GenerateMethods(model, parent, stringGenerator);
        }
    }
}
