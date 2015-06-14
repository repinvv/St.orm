namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Models.Pregen;

    internal class MethodsCollectionFactory
    {
        private IMethodGenerator[] structureMethods;
        private IMethodGenerator[] plainClassMethods;
        private IMethodGenerator[] baseClassMethods;
        private IMethodGenerator[] inheritedClassMethods;

        public MethodsCollectionFactory(StructureMethodsCollection structureMethodsCollection,
            PlainClassMethodsCollection plainClassMethodsCollection,
            BaseClassMethodsCollection baseClassMethodsCollection,
            InheritedClassMethodsCollection inheritedClassMethodsCollection)
        {
            structureMethods = structureMethodsCollection.GetGeneratorsCollection();
            baseClassMethods = baseClassMethodsCollection.GetGeneratorsCollection();
            inheritedClassMethods = inheritedClassMethodsCollection.GetGeneratorsCollection();
            plainClassMethods = plainClassMethodsCollection.GetGeneratorsCollection();
        }

        public IMethodGenerator[] GetRepositoryMethods(Model model)
        {
            if (model.IsStruct)
            {
                return structureMethods;
            }

            if (model.IsInherited)
            {
                return inheritedClassMethods;
            }

            return plainClassMethods;
        }
    }
}
