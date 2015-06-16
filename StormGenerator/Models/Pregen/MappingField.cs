namespace StormGenerator.Models.Pregen
{
    using System;
    using StormGenerator.Models.Config.Db;

    internal class MappingField : Switcheable
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public string CustomTypeName { get; set; }

        public string CustomTypeNamespace { get; set; }

        public DbField DbField { get; set; }

        public override string ToString()
        {
            return "Field: " + Name;
        }
    }
}
