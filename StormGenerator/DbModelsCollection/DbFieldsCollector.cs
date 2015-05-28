namespace StormGenerator.DbModelsCollection
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using StormGenerator.Models.Config.Db;

    internal class DbFieldsCollector
    {
        private readonly ColumnReader columnReader;

        public DbFieldsCollector(ColumnReader columnReader)
        {
            this.columnReader = columnReader;
        }

        public void CollectFields(List<DbModel> models, SqlConnection connection)
        {
            var columns = columnReader.ReadColumns(connection);
        }
    }
}
