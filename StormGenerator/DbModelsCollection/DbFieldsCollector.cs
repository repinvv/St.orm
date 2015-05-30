namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using StormGenerator.Models.Config.Db;

    internal class DbFieldsCollector
    {
        private readonly ColumnReader columnReader;
        private readonly Dictionary<string, Type> dbtypes =
            new Dictionary<string, Type>
            {
                { "image", typeof(byte[]) },
                { "text", typeof(string) },
                { "binary", typeof(byte[]) },
                { "date", typeof(DateTime) },
                { "tinyint", typeof(byte) },
                { "time", typeof(DateTime) },
                { "bir", typeof(bool) },
                { "smallint", typeof(short) },
                { "decimal", typeof(decimal) },
                { "int", typeof(int) },
                { "smalldatetime", typeof(DateTime) },
                { "real", typeof(float) },
                { "float", typeof(double) },
                { "money", typeof(decimal) },
                { "numeric", typeof(decimal) },
                { "smallmoney", typeof(decimal) },
                { "datetime", typeof(DateTime) },
                { "datetime2", typeof(DateTime) },
                { "bigint", typeof(long) },
                { "varbinary", typeof(byte[]) },
                { "timestamp", typeof(DateTime) },
                { "sysname", typeof(string) },
                { "nvarchar", typeof(string) },
                { "varchar", typeof(string) },
                { "ntext", typeof(string) },
                { "uniqueidentifier", typeof(Guid) },
                { "datetimeoffset", typeof(DateTimeOffset) },
                { "sql_variant", typeof(object) },
                { "xml", typeof(string) },
                { "char", typeof(string) },
                { "nchar", typeof(string) }
            };

        public DbFieldsCollector(ColumnReader columnReader)
        {
            this.columnReader = columnReader;
        }

        public void CollectFields(List<DbModel> models, SqlConnection connection)
        {
            var columns = columnReader.ReadColumns(connection);
            var modelDict = models.ToDictionary(x => new { x.Name, x.SchemaName });
            foreach (var column in columns.OrderBy(x => x.Index))
            {
                var key = new { Name = column.TableName, SchemaName = column.TableSchema };
                var model = modelDict[key];
                model.Fields.Add(CreateField(column));
            }
        }

        private DbField CreateField(DbColumn column)
        {
            return new DbField
                   {
                       Name = column.Name,
                       Type = column.Type,
                       IsIdentity = column.IsIdentity,
                       IsNullable = column.IsNullable,
                       IsReadonly = column.IsComputed,
                       Index = column.Index,
                       StringLength = column.StringLength,
                       Associations = new List<DbAssociation>()
                   };
        }

        private Type GetFieldType(string type, bool isNullable)
        {
            try
            {
                var fieldType = dbtypes[type];
                return fieldType.IsValueType && isNullable
                    ? typeof(Nullable<>).MakeGenericType(fieldType)
                    : fieldType;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("type " + type + " is not supported.");
                return typeof(object);
            }
        }
    }
}
