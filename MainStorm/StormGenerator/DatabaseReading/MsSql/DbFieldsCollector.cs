﻿namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using StormGenerator.DatabaseReading.DbModels;

    internal class DbFieldsCollector
    {
        private readonly ColumnReader columnReader;

        public DbFieldsCollector(ColumnReader columnReader)
        {
            this.columnReader = columnReader;
        }

        public void CollectFields(List<Table> models, SqlConnection connection)
        {
            var columns = columnReader.ReadColumns(connection);
            var modelDict = models.ToDictionary(x => x.Id);
            foreach (var column in columns.OrderBy(x => x.Index))
            {
                modelDict[column.TableId].Columns.Add(CreateColumn(column));
            }
        }

        private Column CreateColumn(DbColumn column)
        {
            return new Column
            {
                       Name = column.Name,
                       Type = column.Type,
                       Default = column.Default,
                       IsIdentity = column.IsIdentity,
                       IsNullable = column.IsNullable,
                       IsReadonly = column.IsComputed,
                       Index = column.Index,
                       Length = column.Length,
                       Precision = column.Precision,
                       Scale = column.Scale,
                       Associations = new List<Association>()
                   };
        }
    }
}
