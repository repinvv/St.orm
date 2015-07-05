namespace StormGenerator.Generation.RepositoryGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.ModelGeneration;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryGenerator
    {
        private readonly FileGenerator fileGenerator;
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly RepositoryMethodsGenerator repositoryMethodsGenerator;
        private readonly RepositoryConstructorGenerator repositoryConstructorGenerator;
        private readonly Generics generics;

        public RepositoryGenerator(FileGenerator fileGenerator,
            UsingsGenerator usingsGenerator, 
            FieldUtility fieldUtility,
            RepositoryMethodsGenerator repositoryMethodsGenerator, 
            RepositoryConstructorGenerator repositoryConstructorGenerator,
            Generics generics)
        {
            this.fileGenerator = fileGenerator;
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.repositoryMethodsGenerator = repositoryMethodsGenerator;
            this.repositoryConstructorGenerator = repositoryConstructorGenerator;
            this.generics = generics;
        }

        public GeneratedFile GenerateRepository(Model model, Options options)
        {
            var name = model.Name + GenerationConstants.ModelGeneration.RepositorySuffix;
            return fileGenerator.GenerateFile(name, options, stringGenerator => GenerateDefinition(model, stringGenerator));
        }

        private void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.ActiveSelect(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.RepositoryUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("internal class " + model.Name + GenerationConstants.ModelGeneration.RepositorySuffix
                                       + " : IDalRepository" + generics.Line(model));
            stringGenerator.Braces(() => GenerateRepositoryContent(model, stringGenerator));
        }

        private void GenerateRepositoryContent(Model model, IStringGenerator stringGenerator)
        {
            repositoryConstructorGenerator.GenerateConstructor(model, stringGenerator);
            repositoryMethodsGenerator.GenerateMethods(model, stringGenerator);
        }
    }
}
