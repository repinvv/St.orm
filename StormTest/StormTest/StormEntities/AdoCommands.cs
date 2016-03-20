namespace StormTest.StormEntities
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;

    public static class AdoCommands
    {
        public static List<TDal> Materialize<TDal, TQuery>(IQueryable<TQuery> query,
            DbConnection connection,
            DbTransaction transaction,
            Func<IDataReader, TDal> create)
            where TQuery : class
        {
            return ExecuteSelect(query.ToString(),
                                 connection,
                                 transaction,
                                 create);
        }

        public static List<T> ExecuteSelect<T>(string request,
                                                DbConnection connection,
                                                DbTransaction transaction,
                                                Func<IDataReader, T> itemCreator,
                                                params SqlParameter[] parameters)
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

        public static void SplitRun<T>(List<T> list, Action<List<T>> action, int batchSize = 1000)
        {
            var batchCount = (list.Count / batchSize) + (list.Count % batchSize == 0 ? 0 : 1);
            var outputSize = (list.Count / batchCount) + (list.Count % batchCount == 0 ? 0 : 1);
            for (int i = 0; i < list.Count; i += outputSize)
            {
                action(list.GetRange(i, Math.Min(outputSize, list.Count - i)));
            }
        }

        public static void RunCommand(string request, SqlParameter[] parameters, DbConnection connection, DbTransaction transaction, Action<IDataReader> action)
        {
            using (var command = new SqlCommand(request, (SqlConnection)connection))
            {
                command.Transaction = (SqlTransaction)transaction;
                command.Parameters.AddRange(parameters);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        action(reader);
                    }
                }
            }
        }
    }
}
