namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Persistence
{
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class BulkInsertMethodGenerator : IMethodGenerator
    {
        private readonly PersistenceEventGenerator persistenceEventGenerator;

        public BulkInsertMethodGenerator(PersistenceEventGenerator persistenceEventGenerator)
        {
            this.persistenceEventGenerator = persistenceEventGenerator;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"public void Insert(IStormContext context, IList<{model.Name}> entities)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            persistenceEventGenerator.GeneratePreInsert(model, stringGenerator);
        }
    }
}
