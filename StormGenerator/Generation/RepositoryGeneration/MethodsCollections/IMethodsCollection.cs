namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Models.Pregen;

    internal interface IMethodsCollection
    {
        IMethodGenerator[] GetGeneratorsCollection(Model model);
    }
}
