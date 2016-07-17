namespace StormCITest.Tests
{
    using System.Data.Common;
    using System.Data.SqlClient;
    using StormTestProject.StormSchema;

    internal static class DeleteAll
    {
        public static void EntityWithGuid(DbConnection conn)
        {
            using (new ConnectionHandler(conn))
            {
                var sql = "delete from entity_with_guid";
                CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], (SqlConnection)conn, null);
            }
        }
    }
}
