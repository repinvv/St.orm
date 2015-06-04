namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    internal class ColumnReader
    {
        private const string Query = @"SELECT *,
			COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
			COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed
		FROM  INFORMATION_SCHEMA.COLUMNS";

        private readonly Reader reader;
        private readonly TableIdCreator tableIdCreator;

        public ColumnReader(Reader reader, TableIdCreator tableIdCreator)
        {
            this.reader = reader;
            this.tableIdCreator = tableIdCreator;
        }

        public List<DbColumn> ReadColumns(DbConnection connection)
        {
            Func<IDataReader, DbColumn> func = r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schema = r["TABLE_SCHEMA"] as string;
                var db = r["TABLE_CATALOG"] as string;
                return new DbColumn
                       {
                           TableId = tableIdCreator.CreateTableId(db, schema, name),
                           Name = r["COLUMN_NAME"] as string,
                           Type = r["DATA_TYPE"] as string,
                           Index = (int)r["ORDINAL_POSITION"],
                           IsNullable = r["IS_NULLABLE"] as string == "YES",
                           StringLength = r["CHARACTER_MAXIMUM_LENGTH"] as int? ?? 0,
                           IsIdentity = (int)r["IsIdentity"] == 1,
                           IsComputed = (int)r["IsComputed"] == 1,
                       };
            };
            return reader.Read(connection, Query, func);
        }
    }
}