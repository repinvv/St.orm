namespace StormGenerator.DbModelsCollection
{
    using System.Collections.Generic;
    using StormGenerator.Models.Config.Db;

    internal class DbModelsCollector
    {
        private readonly DbConnectionCreator connectionCreator;
        private readonly TableReader tableReader;

        public DbModelsCollector(DbConnectionCreator connectionCreator, TableReader tableReader)
        {
            this.connectionCreator = connectionCreator;
            this.tableReader = tableReader;
        }

        public List<DbModel> GetModels(Options options)
        {
            using (var connection = connectionCreator.CreateConnection(options))
            {
                connection.Open();
                var models = tableReader.ReadTables(connection);
                return models;
            }
        }
    }
}
