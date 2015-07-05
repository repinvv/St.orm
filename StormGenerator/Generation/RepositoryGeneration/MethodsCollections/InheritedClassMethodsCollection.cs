namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Models.Pregen;

    internal class InheritedClassMethodsCollection : IMethodsCollection
    {
        private readonly CommonMethodsCollection commonMethodsCollection;
        private readonly IMethodGenerator[] generators;

        public InheritedClassMethodsCollection(CommonMethodsCollection commonMethodsCollection,
            FullCreateGenerator fullCreateGenerator)
        {
            this.commonMethodsCollection = commonMethodsCollection;
            generators = new IMethodGenerator[]
                         {
                             fullCreateGenerator
                         };
        }

        public IMethodGenerator[] GetGeneratorsCollection(Model model)
        {
            return commonMethodsCollection.GetGeneratorsCollection(model).Concat(generators).ToArray();
        }
    }
}
