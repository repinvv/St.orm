namespace StormGenerator.DatabaseReading
{
    using StormGenerator.DatabaseReading.MsSql;
    using StormGenerator.Infrastructure;
    using StormGenerator.Settings;

    internal class DbModelsReaderFactory
    {
        private readonly Options options;
        private readonly Container container;

        public DbModelsReaderFactory(Options options, Container container)
        {
            this.options = options;
            this.container = container;
        }

        public IDbModelsReader GetReader()
        {
            return container.Get<MsSqlDbModelsReader>();
        }
    }
}
