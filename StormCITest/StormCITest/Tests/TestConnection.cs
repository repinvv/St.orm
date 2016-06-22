namespace StormCITest.Tests
{
    using System.Data.SqlClient;

    internal class TestConnection
    {
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection("data source=.;initial catalog=StormCI;integrated security=True");
        }
    }
}
