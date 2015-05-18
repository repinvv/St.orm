namespace StormGenerator.Models
{
    using System;
    using StormGenerator.Models.Db;

    public class MappingField
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public DbField DbField { get; set; }

        public override string ToString()
        {
            return "Field: " + Name;
        }
    }
}
