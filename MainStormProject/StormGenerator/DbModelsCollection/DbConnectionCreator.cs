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
                return $"Data Source={options.Options.Server};Database={options.Options.Database};Integrated Security=SSPI";
            }

            return $"Server={options.Options.Server};Database={options.Options.Database};User Id={options.Options.User};Password={options.Options.Server};";
        }
    }
}
