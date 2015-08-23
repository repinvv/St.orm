namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular;
    using StormGenerator.Models.Pregen;

    internal class PlainClassMethodsCollection : IMethodsCollection
    {
        private readonly CommonMethodsCollection commonMethodsCollection;
        private readonly PersistenceMethodCollection persistenceMethodCollection;
        private readonly IMethodGenerator[] generators;

        public PlainClassMethodsCollection(CommonMethodsCollection commonMethodsCollection,
            PersistenceMethodCollection persistenceMethodCollection,
            SaveGenerator saveGenerator,
            UpdateGenerator updateGenerator,
            DeleteGenerator deleteGenerator,
            SaveRelationsGenerator saveRelationsGenerator,
            UpdateRelationsGenerator updateRelationsGenerator,
            DeleteRelationsGenerator deleteRelationsGenerator,
            SetMtoFieldsGenerator setMtoFieldsGenerator,
            EntityChangedGenerator entityChangedGenerator)
        {
            this.commonMethodsCollection = commonMethodsCollection;
            this.persistenceMethodCollection = persistenceMethodCollection;
            generators = new IMethodGenerator[]
                         {
                             saveGenerator,
                             updateGenerator,
                             deleteGenerator,
                             saveRelationsGenerator,
                             updateRelationsGenerator,
                             deleteRelationsGenerator,
                             setMtoFieldsGenerator,
                             entityChangedGenerator
                         };
        }

        public IMethodGenerator[] GetGeneratorsCollection(Model model)
        {
            return commonMethodsCollection.GetGeneratorsCollection(model)
                .Concat(generators)
                .Concat(persistenceMethodCollection.GetGeneratorsCollection(model))
                .ToArray();
        }
    }
}
