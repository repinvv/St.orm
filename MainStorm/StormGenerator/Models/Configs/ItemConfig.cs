namespace StormGenerator.Models.Configs
{
    internal abstract class ItemConfig : Named
    {
        public bool IsEnabled { get; set; }

        public bool IsGenerated { get; set; }
    }
}
