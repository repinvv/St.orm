namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Persistence
{
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class PersistenceEventGenerator
    {
        private readonly OptionsService options;

        public PersistenceEventGenerator(OptionsService options)
        {
            this.options = options;
        }

        public void GeneratePreInsert(Model model, IStringGenerator stringGenerator)
        {
            if (string.IsNullOrWhiteSpace(options.Options.CustomInterfaceForEntities))
            {
                return;
            }

            var left = model.IsStruct ? "entities[index] = " : string.Empty;
            stringGenerator.AppendLine("for (int index = 0; index < entities.Count; index++)");
            stringGenerator.Braces(left + "PersistenceEvents.BeforeInsert(entities[index]);");
            stringGenerator.AppendLine();
        }
    }
}
