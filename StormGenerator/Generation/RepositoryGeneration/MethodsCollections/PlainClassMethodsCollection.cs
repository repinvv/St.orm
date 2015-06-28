namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular;

    internal class PlainClassMethodsCollection : IMethodsCollection
    {
        private readonly IMethodGenerator[] generators;

        public PlainClassMethodsCollection(CommonMethodsCollection commonMethodsCollection,
            SaveGenerator saveGenerator,
            UpdateGenerator updateGenerator,
            DeleteGenerator deleteGenerator,
            SaveRelationsGenerator saveRelationsGenerator,
            UpdateRelationsGenerator updateRelationsGenerator,
            DeleteRelationsGenerator deleteRelationsGenerator,
            SetMtoFieldsGenerator setMtoFieldsGenerator,
            EntityChangedGenerator entityChangedGenerator)
        {
            generators = commonMethodsCollection
                .GetGeneratorsCollection()
                .Concat(new IMethodGenerator[]
                        {
                            saveGenerator,
                            updateGenerator,
                            deleteGenerator,
                            saveRelationsGenerator,
                            updateRelationsGenerator,
                            deleteRelationsGenerator,
                            setMtoFieldsGenerator,
                            entityChangedGenerator
                        })
                .ToArray();
        }

        public IMethodGenerator[] GetGeneratorsCollection()
        {
            return generators;
        }
    }
}
