namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;

    internal class BaseClassMethodsCollection : IMethodsCollection
    {
        public IMethodGenerator[] GetGeneratorsCollection()
        {
            return new IMethodGenerator[] { };
        }
    }
}
