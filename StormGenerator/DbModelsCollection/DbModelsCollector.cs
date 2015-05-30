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
        private readonly ForeignKeyReader foreignKeyReader;

        public DbModelsCollector(DbConnectionCreator connectionCreator, 
            TableReader tableReader, 
            DbFieldsCollector fieldsCollector,
            PrimaryKeyReader primaryKeyReader,
            ForeignKeyReader foreignKeyReader)
        {
            this.connectionCreator = connectionCreator;
            this.tableReader = tableReader;
            this.fieldsCollector = fieldsCollector;
            this.primaryKeyReader = primaryKeyReader;
            this.foreignKeyReader = foreignKeyReader;
        }

        public List<DbModel> GetModels(Options options)
        {
            using (var connection = connectionCreator.CreateConnection(options))
            {
                connection.Open();
                var models = tableReader.ReadTables(connection);
                fieldsCollector.CollectFields(models, connection);
                primaryKeyReader.MarkPrimaryKeys(models, connection);
                foreignKeyReader.GetForeignKeys(models, connection);
                return models;
            }
        }
    }
}
