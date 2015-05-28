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

        private readonly Reader reader;

        public TableReader(Reader reader)
        {
            this.reader = reader;
        }

        public List<DbModel> ReadTables(SqlConnection connection)
        {
            Func<IDataReader, DbModel> func = r => new DbModel
                                                   {
                                                       Name = r["TABLE_NAME"] as string,
                                                       SchemaName = r["TABLE_SCHEMA"] as string,
                                                       IsView = "VIEW" == r["TABLE_TYPE"] as string
                                                   };
            return reader.Read(connection, Query, func);
        }
    }
}
