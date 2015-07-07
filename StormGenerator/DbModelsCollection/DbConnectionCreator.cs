namespace StormGenerator.DbModelsCollection
{
    using System.Data.SqlClient;
    using StormGenerator.Infrastructure;

    internal class DbConnectionCreator
    {
        private readonly OptionsService options;

        public DbConnectionCreator(OptionsService options)
        {
            this.options = options;
        }

        public SqlConnection CreateConnection()
        {
            var connectionString = options.Options.ConnectionString ?? CreateConnectionString();
            return new SqlConnection(connectionString);
        }

        private string CreateConnectionString()
        {
            if (options.Options.IntegratedSecurity)
            {
                return string.Format("Data Source={0};Database={1};Integrated Security=SSPI", options.Options.Server, options.Options.Database);
            }

            return string.Format("Server={0};Database={1};User Id={2};Password={3};", options.Options.Server, options.Options.Database, options.Options.User, options.Options.Server);
        }
    }
}
