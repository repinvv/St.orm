namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Models.Pregen;

    internal class MethodsCollectionFactory
    {
        private readonly StructureMethodsCollection structureMethodsCollection;
        private readonly PlainClassMethodsCollection plainClassMethodsCollection;
        private readonly BaseClassMethodsCollection baseClassMethodsCollection;
        private readonly InheritedClassMethodsCollection inheritedClassMethodsCollection;

        public MethodsCollectionFactory(StructureMethodsCollection structureMethodsCollection,
            PlainClassMethodsCollection plainClassMethodsCollection,
            BaseClassMethodsCollection baseClassMethodsCollection,
            InheritedClassMethodsCollection inheritedClassMethodsCollection)
        {
            this.structureMethodsCollection = structureMethodsCollection;
            this.plainClassMethodsCollection = plainClassMethodsCollection;
            this.baseClassMethodsCollection = baseClassMethodsCollection;
            this.inheritedClassMethodsCollection = inheritedClassMethodsCollection;
        }

        public IMethodGenerator[] GetRepositoryMethods(Model model)
        {
            if (model.IsStruct)
            {
                return structureMethodsCollection.GetGeneratorsCollection(model);
            }

            if (model.IsInherited)
            {
                return inheritedClassMethodsCollection.GetGeneratorsCollection(model);
            }

            return plainClassMethodsCollection.GetGeneratorsCollection(model);
        }
    }
}
