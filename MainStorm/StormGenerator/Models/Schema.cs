namespace StormGenerator.Models
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.DbModels;

    internal class Schema
    {
        public List<Table> Tables { get; set; }

        public List<ModelConfig> Configs { get; set; } 
    }
}
