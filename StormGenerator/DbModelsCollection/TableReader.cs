namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using StormGenerator.Models.Config.Db;

    internal class TableReader
    {
        private string query = @"SELECT *
		FROM  INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'";

        public List<DbModel> ReadTables(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                var output = new List<DbModel>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new DbModel();
                        model.Name = reader["TABLE_NAME"] as string;
                        model.SchemaName = reader["TABLE_SCHEMA"] as string;
                        model.IsView = reader["TABLE_TYPE"] == "VIEW";
                        output.Add(model);
                    }
                }

                return output;
            }
        }
    }
}
