﻿namespace StormGenerator.Models.Config.Db
{
    using System;

    public class DbField
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsIdentity { get; set; }

        public string StorageType { get; set; }

        public int StorageLength { get; set; }

        public bool StorageNullable { get; set; }

        public int Index { get; set; }
    }
}
