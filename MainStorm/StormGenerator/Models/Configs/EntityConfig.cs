namespace StormGenerator.Models.Configs
{
    using System.Collections.Generic;

    internal class ModelConfig
    {
        public string Name { get; set; }

        public List<FieldConfig> Fields { get; set; }

        public List<NavPropConfig> NavPropConfigs { get; set; } 
    }
}
