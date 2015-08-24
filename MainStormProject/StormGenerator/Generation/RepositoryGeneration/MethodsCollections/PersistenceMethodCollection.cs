namespace StormGenerator.Generation.RepositoryGeneration.MethodsCollections
{
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Persistence;
    using StormGenerator.Models.Pregen;

    internal class PersistenceMethodCollection : IMethodsCollection
    {
        private readonly IMethodGenerator[] identityInsertMethods;
        private readonly IMethodGenerator[] bulkInsertMethods;

        public PersistenceMethodCollection(IdentityInsertGenerator identityInsertGenerator,
            IdentityRangeInsertGenerator identityRangeInsertGenerator,
            BulkInsertMethodGenerator bulkInsertMethodGenerator)
        {
            identityInsertMethods = new IMethodGenerator[]
                                    {
                                        identityInsertGenerator,
                                        identityRangeInsertGenerator
                                    };
            bulkInsertMethods = new IMethodGenerator[]
                                {
                                    bulkInsertMethodGenerator
                                };
        }

        public IMethodGenerator[] GetGeneratorsCollection(Model model)
        {
            var keyFields = model.KeyFields();
            if (keyFields.Count == 1 && keyFields[0].DbField.IsIdentity)
            {
                return identityInsertMethods;
            }

            return bulkInsertMethods;
        }
    }
}
