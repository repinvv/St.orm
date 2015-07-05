namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Models.Pregen;

    internal class StructureMethodsCollection : IMethodsCollection
    {
        public IMethodGenerator[] GetGeneratorsCollection(Model model)
        {
            return new IMethodGenerator[] { };
        }
    }
}
