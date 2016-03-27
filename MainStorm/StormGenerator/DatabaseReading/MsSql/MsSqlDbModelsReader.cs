namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Collections.Generic;
    using StormGenerator.DatabaseReading.DbModels;

    internal class MsSqlDbModelsReader : IDbModelsReader
    {
        private readonly DbConnectionCreator connectionCreator;
        private readonly TableReader tableReader;
        private readonly DbFieldsCollector fieldsCollector;
        private readonly PrimaryKeyReader primaryKeyReader;
        private readonly ForeignKeyReader foreignKeyReader;
        private readonly Sequencer sequencer;

        public MsSqlDbModelsReader(DbConnectionCreator connectionCreator,
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

        public List<Table> GetTables()
        {
            using (var connection = connectionCreator.CreateConnection())
            {
                connection.Open();
                var tables = tableReader.ReadTables(connection);
                fieldsCollector.CollectFields(tables, connection);
                primaryKeyReader.MarkPrimaryKeys(tables, connection);
                sequencer.MarkSequences(tables);
                foreignKeyReader.GetForeignKeys(tables, connection);
                return tables;
            }
        }
    }
}
