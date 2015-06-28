namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;

    internal class InheritedClassMethodsCollection : IMethodsCollection
    {
        private readonly IMethodGenerator[] generators;

        public InheritedClassMethodsCollection(CommonMethodsCollection commonMethodsCollection,
            FullCreateGenerator fullCreateGenerator)
        {
            generators = commonMethodsCollection
                .GetGeneratorsCollection()
                .Concat(new IMethodGenerator[]
                        {
                            fullCreateGenerator
                        })
                .ToArray();
        }

        public IMethodGenerator[] GetGeneratorsCollection()
        {
            return generators;
        }
    }
}
