namespace StormGenerator.DbModelsCollection
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using StormGenerator.Models.Config.Db;

    internal class DbFieldsCollector
    {
        private readonly ColumnReader columnReader;
        
        public DbFieldsCollector(ColumnReader columnReader)
        {
            this.columnReader = columnReader;
        }

        public void CollectFields(List<DbModel> models, SqlConnection connection)
        {
            var columns = columnReader.ReadColumns(connection);
            var modelDict = models.ToDictionary(x => x.Id);
            foreach (var column in columns.OrderBy(x => x.Index))
            {
                modelDict[column.TableId].Fields.Add(CreateField(column));
            }
        }

        private DbField CreateField(DbColumn column)
        {
            return new DbField
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
                       Associations = new List<DbAssociation>()
                   };
        }
    }
}
