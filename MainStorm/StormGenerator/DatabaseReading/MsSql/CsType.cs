namespace StormGenerator.DatabaseReading.MsSql
{
    using System;
    using System.Collections.Generic;
    using Storm;

    internal static class SqlCsType
    {
        private static Dictionary<string, Type> typeMapping =
            new Dictionary<string, Type>
            {
                { "bigint", typeof(long) },
                { "int", typeof(int) },
                { "numeric", typeof(decimal) },
                { "bit", typeof(bool) },
                { "smallint", typeof(short) },
                { "decimal", typeof(decimal) },
                { "smallmoney", typeof(decimal) },
                { "tinyint", typeof(byte) },
                { "money", typeof(decimal) },
                { "uniqueidentifier", typeof(Guid) },
                { "float", typeof(float) },
                { "real", typeof(double) },
                { "date", typeof(DateTime) },
                { "time", typeof(DateTime) },
                { "datetimeoffset", typeof(DateTimeOffset) },
                { "datetime", typeof(DateTime) },
                { "datetime2", typeof(DateTime) },
                { "smalldatetime", typeof(DateTime) },
                { "char", typeof(string) },
                { "varchar", typeof(string) },
                { "text", typeof(string) },
                { "nchar", typeof(string) },
                { "nvarchar", typeof(string) },
                { "ntext", typeof(string) },
                { "xml", typeof(string) },
                { "binary", typeof(byte[]) },
                { "varbinary", typeof(byte[]) },
                { "image", typeof(byte[]) },
            };

        public static Type GetCsType(string dbType, bool nullable)
        {
            var type = typeMapping.SafeGet(dbType, typeof(object));
            if (!nullable || !type.IsValueType)
            {
                return type;
            }

            return typeof(Nullable<>).MakeGenericType(type);
        }
    }
}
