namespace StormGenerator.DbModelsCollection
{
    using System.Collections.Generic;
    using StormGenerator.Models.Config.Db;

    internal class DbModelsCollector
    {
        private readonly DbConnectionCreator connectionCreator;
        private readonly TableReader tableReader;
        private readonly DbFieldsCollector fieldsCollector;

        public DbModelsCollector(DbConnectionCreator connectionCreator, TableReader tableReader, DbFieldsCollector fieldsCollector)
        {
            this.connectionCreator = connectionCreator;
            this.tableReader = tableReader;
            this.fieldsCollector = fieldsCollector;
        }

        public List<DbModel> GetModels(Options options)
        {
            using (var connection = connectionCreator.CreateConnection(options))
            {
                connection.Open();
                var models = tableReader.ReadTables(connection);
                fieldsCollector.CollectFields(models, connection);
                return models;
            }
        }
    }
}
