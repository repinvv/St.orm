namespace StormGenerator.DbModelsCollection
{
    using System.Data.SqlClient;

    internal class DbConnectionCreator
    {
        public SqlConnection CreateConnection(Options options)
        {
            var connectionString = options.ConnectionString ?? CreateConnectionString(options);
            return new SqlConnection(connectionString);
        }

        private string CreateConnectionString(Options options)
        {
            if (options.IntegratedSecurity)
            {
                return string.Format("Data Source={0};Database={1};Integrated Security=SSPI", options.Server, options.Database);
            }

            return string.Format("Server={0};Database={1};User Id={2};Password={3};", options.Server, options.Database, options.User, options.Server);
        }
    }
}
