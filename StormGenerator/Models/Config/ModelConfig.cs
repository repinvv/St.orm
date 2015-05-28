namespace StormGenerator.Models.Config
{
    using System.Collections.Generic;

    public class ModelConfig
    {
        public string Name { get; set; }

        public string DbModelName { get; set; }

        public bool IsStructure { get; set; }

        public List<FieldConfig> Fields { get; set; } 
    }
}
