namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using StormGenerator.Models.Config.Db;

    internal class PrimaryKeyReader
    {
        private const string Query = @"
				SELECT
					k.TABLE_SCHEMA,
                    k.TABLE_NAME,
					k.COLUMN_NAME
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

        public PrimaryKeyReader(Reader reader)
        {
            this.reader = reader;
        }

        public void MarkPrimaryKeys(List<DbModel> models, DbConnection connection)
        {
            var modelDict = models.ToDictionary(x => new { x.Name, x.SchemaName });
            Action<IDataReader> func =
            r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schemaName = r["TABLE_SCHEMA"] as string;
                var key = new { Name = name, SchemaName = schemaName };
                var model = modelDict[key];
                var columnName = r["COLUMN_NAME"] as string;
                model.Fields.First(x => x.Name == columnName).IsPrimaryKey = true;
            };
            reader.Read(connection, Query, func);
        }
    }
}
