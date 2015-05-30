namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using StormGenerator.Models.Config.Db;

    internal class ForeignKeyReader
    {
        private const string Query = @"SELECT
	fk.TABLE_SCHEMA,
	fk.TABLE_NAME,
	fk.COLUMN_NAME,
	pk.TABLE_SCHEMA as OTHER_SCHEMA,
	pk.TABLE_NAME as OTHER_TABLE,
    pk.COLUMN_NAME as OTHER_COLUMN
				FROM
					INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
					JOIN
						INFORMATION_SCHEMA.KEY_COLUMN_USAGE fk
					ON
						fk.CONSTRAINT_CATALOG = rc.CONSTRAINT_CATALOG AND
						fk.CONSTRAINT_SCHEMA  = rc.CONSTRAINT_SCHEMA  AND
						fk.CONSTRAINT_NAME    = rc.CONSTRAINT_NAME
					JOIN
						INFORMATION_SCHEMA.KEY_COLUMN_USAGE pk
					ON
						pk.CONSTRAINT_CATALOG = rc.UNIQUE_CONSTRAINT_CATALOG AND
						pk.CONSTRAINT_SCHEMA  = rc.UNIQUE_CONSTRAINT_SCHEMA  AND
						pk.CONSTRAINT_NAME    = rc.UNIQUE_CONSTRAINT_NAME
				WHERE
					fk.ORDINAL_POSITION = pk.ORDINAL_POSITION";

        private readonly Reader reader;

        public ForeignKeyReader(Reader reader)
        {
            this.reader = reader;
        }

        public void GetForeignKeys(List<DbModel> models, SqlConnection connection)
        {
            var modelDict = models.ToDictionary(x => new { x.Name, x.SchemaName });
            Action<IDataReader> func = r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schemaName = r["TABLE_SCHEMA"] as string;
                var key = new { Name = name, SchemaName = schemaName };
                var model = modelDict[key];
                var columnName = r["COLUMN_NAME"] as string;
                var association = new DbAssociation
                                  {
                                      Schema = r["OTHER_SCHEMA"] as string,
                                      Table = r["OTHER_TABLE"] as string,
                                      Field = r["OTHER_COLUMN"] as string
                                  };
                model.Fields.First(x => x.Name == columnName).Associations.Add(association);
            };
            reader.Read(connection, Query, func);
        }
    }
}
