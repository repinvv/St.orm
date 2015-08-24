namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class InitializerStartingLine
    {
        public void CreateInitializerStartingLine(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"modelBuilder.Entity<{model.Name}>()");
        }
    }
}
