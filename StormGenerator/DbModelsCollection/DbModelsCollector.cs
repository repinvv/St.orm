namespace StormGenerator.DbModelsCollection
{
    using System.Collections.Generic;
    using StormGenerator.Models.Config.Db;

    internal class DbModelsCollector
    {
        private readonly DbConnectionCreator connectionCreator;
        private readonly TableReader tableReader;
        private readonly DbFieldsCollector fieldsCollector;
        private readonly PrimaryKeyReader primaryKeyReader;

        public DbModelsCollector(DbConnectionCreator connectionCreator, 
            TableReader tableReader, 
            DbFieldsCollector fieldsCollector,
            PrimaryKeyReader primaryKeyReader)
        {
            this.connectionCreator = connectionCreator;
            this.tableReader = tableReader;
            this.fieldsCollector = fieldsCollector;
            this.primaryKeyReader = primaryKeyReader;
        }

        public List<DbModel> GetModels(Options options)
        {
            using (var connection = connectionCreator.CreateConnection(options))
            {
                connection.Open();
                var models = tableReader.ReadTables(connection);
                fieldsCollector.CollectFields(models, connection);
                primaryKeyReader.MarkPrimaryKeys(models, connection);
                return models;
            }
        }
    }
}
