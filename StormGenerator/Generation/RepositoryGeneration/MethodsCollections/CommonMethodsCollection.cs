namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;

    internal class CommonMethodsCollection : IMethodsCollection
    {
        private readonly IMethodGenerator[] generators;

        public CommonMethodsCollection(SetExtensionGenerator setExtensionGenerator,
            RelationsCountGenerator relationsCountGenerator,
            CreateGenerator createGenerator,
            CloneGenerator cloneGenerator,
            MaterializeGenerator materializeGenerator,
            GetByIdQueryGenerator getByIdQueryGenerator)
        {
            generators = new IMethodGenerator[]
                         {
                             setExtensionGenerator,
                             relationsCountGenerator,
                             createGenerator,
                             cloneGenerator,
                             materializeGenerator,
                             getByIdQueryGenerator
                         };
        }

        public IMethodGenerator[] GetGeneratorsCollection()
        {
            return generators;
        }
    }
}
