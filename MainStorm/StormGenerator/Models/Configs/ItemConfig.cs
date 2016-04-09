namespace StormGenerator.Models.Configs
{
    internal abstract class ItemConfig
    {
        public bool IsEnabled { get; set; }

        public bool IsGenerated { get; set; }

        public string Name { get; set; }
    }
}
