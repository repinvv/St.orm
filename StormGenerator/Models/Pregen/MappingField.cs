namespace StormGenerator.Models.Pregen
{
    using System;
    using StormGenerator.Models.Config.Db;

    internal class MappingField
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
