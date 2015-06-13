namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryMethodsGenerator
    {
        private readonly MethodGenerator[] generators;

        public RepositoryMethodsGenerator(SetExtensionGenerator setExtensionGenerator,
            RelationsCountGenerator relationsCountGenerator,
            CreateGenerator createGenerator,
            FullCreateGenerator fullCreateGenerator,
            CloneGenerator cloneGenerator,
            MaterializeGenerator materializeGenerator)
        {
            generators = new MethodGenerator[]
                         {
                             setExtensionGenerator,
                             relationsCountGenerator,
                             cloneGenerator,
                             createGenerator,
                             fullCreateGenerator,
                             materializeGenerator
                         };
        }

        public void GenerateMethods(Model model, Model parent, IStringGenerator stringGenerator)
        {
            foreach (var generator in generators)
            {
                if (!generator.IsNeeded(model))
                {
                    continue;
                }

                stringGenerator.AppendLine();
                generator.GenerateSignature(model, parent, stringGenerator);
                stringGenerator.Braces(() => generator.GenerateMethod(model, parent, stringGenerator));
            }
        }
    }
}
