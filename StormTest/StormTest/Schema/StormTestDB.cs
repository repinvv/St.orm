using System.Data.Common;
using LinqToDB.DataProvider.SqlServer;

namespace StormTest.Schema
{
    public partial class StormTestDB
    {
        public StormTestDB(DbConnection connection)
            : base(new SqlServerDataProvider("StormTest", SqlServerVersion.v2012), connection)
        {
            
        }
    }
}
