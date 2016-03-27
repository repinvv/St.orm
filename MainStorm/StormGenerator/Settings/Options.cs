namespace StormGenerator.Settings
{
    internal class Options
    {
        public string ConnectionString { get; set; }

        public DbConnectionInfo ConnectionInfo { get; set; }

        public bool ForceRefreshDbInfo { get; set; }

        public GenerationOptions Generation { get; set; }
    }
}
