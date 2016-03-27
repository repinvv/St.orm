namespace StormGenerator.DatabaseReading.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using StormGenerator.DatabaseReading.DbModels;

    internal class TableReader
    {
        private const string Query = @"SELECT *
    FROM  INFORMATION_SCHEMA.TABLES
    WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'";

        private readonly Reader reader;
        private readonly TableIdCreator tableIdCreator;

        public TableReader(Reader reader, TableIdCreator tableIdCreator)
        {
            this.reader = reader;
            this.tableIdCreator = tableIdCreator;
        }

        public List<Table> ReadTables(SqlConnection connection)
        {
            Func<IDataReader, Table> func = r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schema = r["TABLE_SCHEMA"] as string;
                return new Table
                {
                    Name = name,
                    Schema = schema,
                    Id = tableIdCreator.CreateTableId(schema, name),
                    IsView = "VIEW" == r["TABLE_TYPE"] as string,
                    Columns = new List<Column>()
                };
            };
            return reader.Read(connection, Query, func);
        }
    }
}
