namespace StormGenerator.Generation.Models
{
    using System.Collections.Generic;

    internal class EntityConfig
    {
        public string Name { get; set; }

        public List<FieldConfig> Fields { get; set; }

        public List<NavPropConfig> NavPropConfigs { get; set; } 
    }
}
