namespace StormGenerator.Models.Configs
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.NavPropConfigs;

    internal class ModelConfig : ItemConfig
    {
        public string Id => string.IsNullOrEmpty(NamespaceSuffix)
            ? Name
            : NamespaceSuffix + "." + Name;

        public string NamespaceSuffix { get; set; }

        public string DbTableId { get; set; }

        public List<FieldConfig> Fields { get; set; }

        public List<NavPropConfig> NavProps { get; set; } 
    }
}
