namespace StormGenerator.Settings
{
    internal class GenOptions
    {
        public Linq2EntitiesProvider Linq2EntitiesProvider { get; set; }

        public DbProvider DbProvider { get; set; }

        public string OutputNamespace { get; set; }

        public string Visibility { get; set; }

        public string ContextName { get; set; }

        public string CustomInterfaceForEntities { get; set; }
    }
}
