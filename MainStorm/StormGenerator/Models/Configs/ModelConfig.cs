namespace StormGenerator.Models.Configs
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.RelationConfigs;

    internal class ModelConfig : ItemConfig
    {
        public string Id => string.IsNullOrEmpty(NamespaceSuffix)
            ? Name
            : NamespaceSuffix + "." + Name;

        public string NamespaceSuffix { get; set; }

        public bool IsStruct { get; set; }

        public string DbTableId { get; set; }

        public List<FieldConfig> Fields { get; set; }

        public List<RelationConfig> Relations { get; set; }
    }
}
