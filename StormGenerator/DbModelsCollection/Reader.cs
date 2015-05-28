namespace StormGenerator.DbModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    internal class Reader
    {
        public List<T> Read<T>(DbConnection connection, string query, Func<IDataReader, T> func)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                var output = new List<T>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        output.Add(func(reader));
                    }
                }

                return output;
            }
        }
    }
}
