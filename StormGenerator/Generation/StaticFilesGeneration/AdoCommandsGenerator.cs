namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class AdoCommandsGenerator : IStaticFileGenerator
    {
        public string GetName(Options options)
        {
            return "Storm.AdoCommands";
        }

        public void GenerateContent(List<Model> models, Options options, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using EntityFramework.Extensions;

    public static class AdoCommands
    {
        public static List<TDal> Materialize<TDal, TQuery>(IQueryable<TQuery> source, Func<IDataReader, TDal> itemCreator, DbConnection connection, DbTransaction transaction)
            where TQuery : class
        {
            var objectQuery = source.ToObjectQuery();
            return ExecuteSelect(objectQuery.ToTraceString(),
                                 itemCreator,
                                 connection,
                                 transaction,
                                 objectQuery.Parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray());
        }

        private static List<T> ExecuteSelect<T>(string request, Func<IDataReader, T> itemCreator, DbConnection connection, DbTransaction transaction, params SqlParameter[] parameters)
        {
            using (new ConnectionHandler(connection))
            {
                using (var command = new SqlCommand(request, (SqlConnection)connection))
                {
                    command.Transaction = transaction as SqlTransaction;
                    command.Parameters.AddRange(parameters);
                    using (var reader = command.ExecuteReader())
                    {
                        var list = new List<T>();
                        while (reader.Read())
                        {
                            list.Add(itemCreator(reader));
                        }

                        return list;
                    }
                }
            }
        }
    }");
        }
    }
}
