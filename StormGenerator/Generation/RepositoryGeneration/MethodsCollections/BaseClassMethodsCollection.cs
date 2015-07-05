namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Models.Pregen;

    internal class BaseClassMethodsCollection : IMethodsCollection
    {
        public IMethodGenerator[] GetGeneratorsCollection(Model model)
        {
            return new IMethodGenerator[] { };
        }
    }
}
