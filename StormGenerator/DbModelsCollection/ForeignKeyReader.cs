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
    rc.CONSTRAINT_SCHEMA,
    rc.CONSTRAINT_NAME,
    rc.UPDATE_RULE,
    rc.DELETE_RULE,
    pk.ORDINAL_POSITION,
    pk.TABLE_SCHEMA as OTHER_SCHEMA,
    pk.TABLE_NAME as OTHER_TABLE,
    pk.COLUMN_NAME as OTHER_COLUMN
        FROM
            INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
            JOIN
                INFORMATION_SCHEMA.KEY_COLUMN_USAGE fk
            ON
                fk.CONSTRAINT_SCHEMA  = rc.CONSTRAINT_SCHEMA  AND
                fk.CONSTRAINT_NAME    = rc.CONSTRAINT_NAME
            JOIN
                INFORMATION_SCHEMA.KEY_COLUMN_USAGE pk
            ON
                pk.CONSTRAINT_SCHEMA  = rc.UNIQUE_CONSTRAINT_SCHEMA  AND
                pk.CONSTRAINT_NAME    = rc.UNIQUE_CONSTRAINT_NAME
        WHERE
            fk.ORDINAL_POSITION = pk.ORDINAL_POSITION";

        private readonly Reader reader;
        private readonly TableIdCreator tableIdCreator;

        public ForeignKeyReader(Reader reader, TableIdCreator tableIdCreator)
        {
            this.reader = reader;
            this.tableIdCreator = tableIdCreator;
        }

        public void GetForeignKeys(List<DbModel> models, SqlConnection connection)
        {
            var modelDict = models.ToDictionary(x => x.Id);
            Action<IDataReader> func = r =>
            {
                var name = r["TABLE_NAME"] as string;
                var schema = r["TABLE_SCHEMA"] as string;

                var model = modelDict[tableIdCreator.CreateTableId(schema, name)];
                var columnName = r["COLUMN_NAME"] as string;

                name = r["OTHER_TABLE"] as string;
                schema = r["OTHER_SCHEMA"] as string;
                var constraintSchema = r["CONSTRAINT_SCHEMA"] as string;
                var constraintName = r["CONSTRAINT_NAME"] as string;

                var association = new DbAssociation
                                  {
                                      ConstraintId = tableIdCreator.CreateTableId(constraintSchema, constraintName),
                                      TableId = tableIdCreator.CreateTableId(schema, name),
                                      FieldName = r["OTHER_COLUMN"] as string,
                                      Index = (int)r["ORDINAL_POSITION"],
                                      Cascade = "CASCADE".Equals(r["DELETE_RULE"])
                                  };
                model.Fields.First(x => x.Name == columnName).Associations.Add(association);
            };
            reader.Read(connection, Query, func);
        }
    }
}
