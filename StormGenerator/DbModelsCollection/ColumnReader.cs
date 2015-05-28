namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Data;
    using System.Data.Common;

    internal class ColumnReader
    {
        private const string Query = @"SELECT *,
			COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
			COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed
		FROM  INFORMATION_SCHEMA.COLUMNS";

        private readonly Reader reader;

        public ColumnReader(Reader reader)
        {
            this.reader = reader;
        }

        public object ReadColumns(DbConnection connection)
        {
            Func<IDataReader, DbColumn> func = r =>
                new DbColumn
                {
                    TableSchema = r["TABLE_SCHEMA"] as string,
                    TableName = r["TABLE_NAME"] as string,
                    Name = r["COLUMN_NAME"] as string,
                    Type = r["DATA_TYPE"] as string,
                    Index = (int)r["ORDINAL_POSITION"],
                    IsNullable = (bool)r["IS_NULLABLE"],
                    StringLength = r["CHARACTER_MAXIMUM_LENGTH"] as int? ?? 0,
                    IsIdentity = (bool)r["IsIdentity"],
                    IsComputed = (bool)r["IsComputed"],
                };
            return reader.Read(connection, Query, func);
        }
    }
}
