namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using StormGenerator.Models.DbModels;

    internal class DbFieldsCollector
    {
        private readonly ColumnReader columnReader;
        private readonly ColumnDefinition columnDefinition;

        public DbFieldsCollector(ColumnReader columnReader, ColumnDefinition columnDefinition)
        {
            this.columnReader = columnReader;
            this.columnDefinition = columnDefinition;
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
            var csType = SqlCsType.GetCsType(column.Type, column.IsNullable);
            return new Column
            {
                       Name = column.Name,
                       DbType = column.Type,
                       CsType = csType,
                       CsTypeName = csType.GetTypeName(),
                       Default = column.Default,
                       IsIdentity = column.IsIdentity,
                       IsNullable = column.IsNullable,
                       IsReadonly = column.IsComputed,
                       Index = column.Index,
                       Length = column.Length,
                       Precision = column.Precision,
                       Scale = column.Scale,
                       Definition = columnDefinition.GetColumnDefinition(column),
                       Associations = new List<Association>()
                   };
        }
    }
}
