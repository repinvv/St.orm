namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ConnectionHandlerGenerator : IStaticFileGenerator
    {
        public string GetName()
        {
            return "Storm.ConnectionHandler";
        }

        public void GenerateContent(List<Model> models, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System;
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
    }");
        }
    }
}
