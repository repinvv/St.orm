namespace StormGenerator.Models.DbModels
{
    using System;
    using System.Collections.Generic;

    internal class Column
    {
        public string Name { get; set; }

        public string DbType { get; set; }

        public Type CsType { get; set; }

        public string CsTypeName { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsIdentity { get; set; }

        public string Default { get; set; }

        public int Index { get; set; }

        public bool IsReadonly { get; set; }

        public int Length { get; set; }

        public int Precision { get; set; }

        public int Scale { get; set; }

        public string Definition { get; set; }

        public string Sequence { get; set; }

        public List<Association> Associations { get; set; }
    }
}
