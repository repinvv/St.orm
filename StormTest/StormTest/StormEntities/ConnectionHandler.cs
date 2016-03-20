namespace StormTest.StormEntities
{
    using System;
    using System.Data;
    using System.Data.Common;

    public class ConnectionHandler : IDisposable
    {
        private readonly DbConnection connection;

        public ConnectionHandler(DbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                this.connection = connection;
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
