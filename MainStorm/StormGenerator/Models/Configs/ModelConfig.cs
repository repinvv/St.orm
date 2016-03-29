namespace StormGenerator.Models.Configs
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.NovPropConfigs;

    internal class ModelConfig : ItemConfig
    {
        public string Name { get; set; }

        public string NamespaceSuffix { get; set; }

        public string DbTableId { get; set; }

        public List<FieldConfig> Fields { get; set; }

        public List<NavPropConfig> NavProps { get; set; } 
    }
}
