namespace StormGenerator.DatabaseReading
{
    using StormGenerator.DatabaseReading.MsSql;
    using StormGenerator.Infrastructure;
    using StormGenerator.Settings;

    internal class DbModelsReaderFactory
    {
        private readonly Options options;
        private readonly IResolve resolve;

        public DbModelsReaderFactory(Options options, IResolve resolve)
        {
            this.options = options;
            this.resolve = resolve;
        }

        public IDbModelsReader GetReader()
        {
            return resolve.Get<MsSqlDbModelsReader>();
        }
    }
}
