namespace StormGenerator.DatabaseReading.MsSql
{
    using System.Data.SqlClient;
    using StormGenerator.Settings;

    internal class DbConnectionCreator
    {
        private readonly Options options;

        public DbConnectionCreator(Options options)
        {
            this.options = options;
        }

        public SqlConnection CreateConnection()
        {
            var connectionString = options.ConnectionString ?? CreateConnectionString(options.ConnectionInfo);
            return new SqlConnection(connectionString);
        }

        private string CreateConnectionString(DbConnectionInfo info)
        {
            if (info.IntegratedSecurity)
            {
                return $"Data Source={info.Server};Database={info.Database};Integrated Security=SSPI";
            }

            return $"Server={info.Server};Database={info.Database};User Id={info.User};Password={info.Server};";
        }
    }
}
