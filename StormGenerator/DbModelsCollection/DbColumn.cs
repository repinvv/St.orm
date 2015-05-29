﻿namespace StormGenerator.DbModelsCollection
{
    internal class DbColumn
    {
        public string TableSchema { get; set; }

        public string TableName { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Index { get; set; }

        public bool IsNullable { get; set; }

        public int StringLength { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsComputed { get; set; }
    }
}