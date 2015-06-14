namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;

    internal interface IMethodsCollection
    {
        IMethodGenerator[] GetGeneratorsCollection();
    }
}
