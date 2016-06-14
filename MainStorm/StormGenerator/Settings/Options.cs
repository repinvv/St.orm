namespace StormGenerator.Settings
{
    internal class Options
    {
        public string ConnectionString { get; set; }

        public DbConnectionInfo ConnectionInfo { get; set; }

        public bool ForceRefreshDbInfo { get; set; }

        public bool AutomaticPopulation { get; set; }

        public AutomaticPopulationOptions AutomaticPopulationOptions { get; set; }

        public GenOptions GenOptions { get; set; }
    }
}
