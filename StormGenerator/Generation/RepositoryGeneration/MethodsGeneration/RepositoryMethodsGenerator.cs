namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryMethodsGenerator
    {
        private IMethodGenerator[] generators;

        public RepositoryMethodsGenerator(RelationsCountGenerator relationsCountGenerator)
        {
            generators = new IMethodGenerator[]
                         {
                             relationsCountGenerator
                         };
        }

        public void GenerateMethods(Model model, Model parent, IStringGenerator stringGenerator)
        {
            foreach (var generator in generators)
            {
                stringGenerator.AppendLine();
                generator.GenerateSignature(model, parent, stringGenerator);
                stringGenerator.Braces(() => generator.GenerateMethod(model, parent, stringGenerator));
            }
        }
    }
}
