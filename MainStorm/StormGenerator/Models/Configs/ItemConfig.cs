namespace StormGenerator.Models.Configs
{
    internal abstract class ItemConfig
    {
        public string Name { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsGenerated { get; set; }
    }
}
