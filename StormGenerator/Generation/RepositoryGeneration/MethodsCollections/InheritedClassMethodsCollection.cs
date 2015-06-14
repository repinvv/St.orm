namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;

    internal class InheritedClassMethodsCollection : IMethodsCollection
    {
        private IMethodGenerator[] generators;

        public InheritedClassMethodsCollection(SetExtensionGenerator setExtensionGenerator,
            RelationsCountGenerator relationsCountGenerator,
            CreateGenerator createGenerator,
            FullCreateGenerator fullCreateGenerator,
            CloneGenerator cloneGenerator,
            MaterializeGenerator materializeGenerator)
        {
            generators = new IMethodGenerator[]
                         {
                             setExtensionGenerator,
                             relationsCountGenerator,
                             cloneGenerator,
                             createGenerator,
                             fullCreateGenerator,
                             materializeGenerator
                         };
        }

        public IMethodGenerator[] GetGeneratorsCollection()
        {
            return generators;
        }
    }
}
