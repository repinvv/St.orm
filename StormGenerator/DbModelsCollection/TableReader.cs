namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using StormGenerator.Models.Config.Db;

    internal class TableReader
    {
        private const string Query = @"SELECT *
		FROM  INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'";

        private readonly TableIdCreator tableIdCreator;
        private readonly Reader reader;

        public TableReader(Reader reader, TableIdCreator tableIdCreator)
        {
            this.reader = reader;
            this.tableIdCreator = tableIdCreator;
        }

        public List<DbModel> ReadTables(SqlConnection connection)
        {
            Func<IDataReader, DbModel> func = r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schema = r["TABLE_SCHEMA"] as string;
                var db = r["TABLE_CATALOG"] as string;
                return new DbModel
                {
                    Name = name,
                    Id = tableIdCreator.CreateTableId(db, schema, name),
                    IsView = "VIEW" == r["TABLE_TYPE"] as string,
                    Fields = new List<DbField>()
                };
            };
            return reader.Read(connection, Query, func);
        }
    }
}
