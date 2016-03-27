namespace StormGenerator.Generation.Models
{
    using System.Collections.Generic;
    using StormGenerator.DatabaseReading.DbModels;

    internal class Schema
    {
        public List<Table> Tables { get; set; }

        public List<EntityConfig> Configs { get; set; } 
    }
}
