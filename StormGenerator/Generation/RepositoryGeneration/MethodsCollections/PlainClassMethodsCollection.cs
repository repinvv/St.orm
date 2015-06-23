namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;

    internal class PlainClassMethodsCollection : IMethodsCollection
    {
        private readonly IMethodGenerator[] generators;

        public PlainClassMethodsCollection(SetExtensionGenerator setExtensionGenerator,
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
                             cloneGenerator,
                             createGenerator,
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
