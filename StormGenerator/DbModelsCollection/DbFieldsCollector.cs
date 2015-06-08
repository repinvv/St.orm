namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text.RegularExpressions;
    using StormGenerator.Models.Config.Db;

    internal class DbFieldsCollector
    {
        private readonly Regex regex = new Regex(@"NEXT VALUE FOR \[(.*)\]");
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
            var match = regex.Match(column.Default ?? string.Empty);
            var sequence = match.Success ? match.Groups[1].Value : null;
            return new DbField
                   {
                       Name = column.Name,
                       Type = column.Type,
                       IsIdentity = column.IsIdentity,
                       Sequence = sequence,
                       IsNullable = column.IsNullable,
                       IsReadonly = column.IsComputed,
                       Index = column.Index,
                       StringLength = column.StringLength,
                       Associations = new List<DbAssociation>()
                   };
        }
    }
}
