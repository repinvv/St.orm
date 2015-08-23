namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Persistence
{
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class IdentityInsertGenerator : IMethodGenerator
    {
        private readonly SplitRunCount splitRunCount;
        private readonly PersistenceEventGenerator persistenceEventGenerator;

        public IdentityInsertGenerator(SplitRunCount splitRunCount, PersistenceEventGenerator persistenceEventGenerator)
        {
            this.splitRunCount = splitRunCount;
            this.persistenceEventGenerator = persistenceEventGenerator;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"public void Insert(IStormContext context, IList<{model.Name}> entities)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            persistenceEventGenerator.GeneratePreInsert(model, stringGenerator);
            stringGenerator.AppendLine("using (new ConnectionHandler(context.Connection))");
            stringGenerator.Braces("AdoCommands.SplitRun(entities.AsList(), " +
                                   $"x => RangeInsert(x, context.Connection, context.Transaction), {splitRunCount.Get(model)});");
        }
    }
}
