namespace StormGenerator.Model
{
    using System;
    using StormGenerator.Model.Db;

    internal class MappingField : Field
    {
        public bool IsSystemType { get; set; }

        public DbField DbField { get; set; }

        public Type FieldType { get; set; }

        public string FieldTypeName { get; set; }
    }
}
