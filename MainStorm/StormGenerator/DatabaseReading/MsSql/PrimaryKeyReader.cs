namespace StormGenerator.DatabaseReading.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using StormGenerator.Models.DbModels;

    internal class PrimaryKeyReader
    {
        private const string Query =
@"SELECT k.*
  FROM
   INFORMATION_SCHEMA.KEY_COLUMN_USAGE k
   JOIN
     INFORMATION_SCHEMA.TABLE_CONSTRAINTS c
   ON
     k.CONSTRAINT_CATALOG = c.CONSTRAINT_CATALOG AND
     k.CONSTRAINT_SCHEMA  = c.CONSTRAINT_SCHEMA AND
     k.CONSTRAINT_NAME    = c.CONSTRAINT_NAME
  WHERE
    c.CONSTRAINT_TYPE='PRIMARY KEY'";

        private readonly Reader reader;
        private readonly TableIdCreator tableIdCreator;

        public PrimaryKeyReader(Reader reader, TableIdCreator tableIdCreator)
        {
            this.reader = reader;
            this.tableIdCreator = tableIdCreator;
        }

        public void MarkPrimaryKeys(List<Table> models, DbConnection connection)
        {
            var modelDict = models.ToDictionary(x => x.Id);
            Action<IDataReader> func =
            r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schema = r["TABLE_SCHEMA"] as string;
                var model = modelDict[tableIdCreator.CreateTableId(schema, name)];
                var columnName = r["COLUMN_NAME"] as string;
                model.Columns.First(x => x.Name == columnName).IsPrimaryKey = true;
            };
            reader.Read(connection, Query, func);
        }
    }
}
