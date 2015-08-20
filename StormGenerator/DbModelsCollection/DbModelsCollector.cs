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
        private readonly Sequencer sequencer;

        public DbModelsCollector(
            DbConnectionCreator connectionCreator,
            TableReader tableReader,
            DbFieldsCollector fieldsCollector,
            PrimaryKeyReader primaryKeyReader,
            ForeignKeyReader foreignKeyReader,
            Sequencer sequencer)
        {
            this.connectionCreator = connectionCreator;
            this.tableReader = tableReader;
            this.fieldsCollector = fieldsCollector;
            this.primaryKeyReader = primaryKeyReader;
            this.foreignKeyReader = foreignKeyReader;
            this.sequencer = sequencer;
        }

        public List<DbModel> GetModels()
        {
            using (var connection = connectionCreator.CreateConnection())
            {
                connection.Open();
                var models = tableReader.ReadTables(connection);
                fieldsCollector.CollectFields(models, connection);
                primaryKeyReader.MarkPrimaryKeys(models, connection);
                sequencer.MarkSequences(models);
                foreignKeyReader.GetForeignKeys(models, connection);
                return models;
            }
        }
    }
}
