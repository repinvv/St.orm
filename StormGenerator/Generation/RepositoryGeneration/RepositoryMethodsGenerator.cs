﻿namespace StormGenerator.Generation.RepositoryGeneration
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsCollections;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RepositoryMethodsGenerator
    {
        private readonly MethodsCollectionFactory methodsCollectionFactory;

        public RepositoryMethodsGenerator(MethodsCollectionFactory methodsCollectionFactory)
        {
            this.methodsCollectionFactory = methodsCollectionFactory;
        }

        public void GenerateMethods(Model model, Model parent, IStringGenerator stringGenerator)
        {
            foreach (var generator in methodsCollectionFactory.GetRepositoryMethods(model))
            {
                stringGenerator.AppendLine();
                generator.GenerateSignature(model, parent, stringGenerator);
                stringGenerator.Braces(() => generator.GenerateMethod(model, parent, stringGenerator));
            }
        }
    }
}
