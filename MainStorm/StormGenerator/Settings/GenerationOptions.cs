namespace StormGenerator.Settings
{
    internal class GenerationOptions
    {
        public Linq2EntitiesProvider Linq2EntitiesProvider { get; set; }

        public string OutputNamespace { get; set; }

        public string ContextName { get; set; }

        public string CustomInterfaceForEntities { get; set; }
    }
}
