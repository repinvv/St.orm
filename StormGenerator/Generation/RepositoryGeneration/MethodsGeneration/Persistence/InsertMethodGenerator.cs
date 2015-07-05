namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Persistence
{
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class BulkInsertMethodGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void Insert(IStormContext context, IList<" + model.Name + "> entities)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            var keyFields = model.MappingFields.Where(x => x.DbField.IsPrimaryKey).ToList();
            if (keyFields.Count == 1 && keyFields[0].DbField.IsIdentity)
            {
                GenerateIdentityInsert(model, keyFields[0], stringGenerator);
            }
        }

        private void GenerateIdentityInsert(Model model, MappingField identity, IStringGenerator stringGenerator)
        {
        }
    }
}
