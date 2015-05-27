namespace StormGenerator.Models
{
    using System.Collections.Generic;
    using StormGenerator.Models.Db;
    using StormGenerator.Models.Relation;

    internal class Model
    {
        public string Name { get; set; }

        public DbModel DbModel { get; set; }

        public List<MappingField> MappingFields { get; set; }

        public List<RelationField> RelationFields { get; set; }

        public override string ToString()
        {
            return "Model: " + Name;
        }
    }
}
