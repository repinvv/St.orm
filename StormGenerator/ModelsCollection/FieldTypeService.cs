namespace StormGenerator.ModelsCollection
{
    using System;
    using System.Collections.Generic;

    internal class FieldTypeService
    {
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

        public Type GetFieldType(string type, bool isNullable)
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
