namespace StormCITest.Tests
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Transactions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormModel;

    [TestClass]
    public class ReadTests
    {
        [TestMethod]
        public void ReadEntityWithGuid_AllEntries()
        {
            //using (var trans = new TransactionScope(TransactionScopeOption.Required))
            {
                var efEntity = new entity_with_guid
                {
                    id = Guid.NewGuid(),
                    a_date = new DateTime(2016, 1, 1, 1, 2, 3),
                    a_datetime = new DateTime(2016, 1, 2, 1, 2, 3),
                    a_datetime2 = new DateTime(2016, 1, 3, 1, 2, 3),
                    a_float = 123.456,
                    a_offset = new DateTimeOffset(2016, 1, 4, 1,2,3, TimeSpan.FromMinutes(10)),
                    a_real = (float)234.567,
                    a_smalldatetime = new DateTime(2016, 1, 5, 1, 2, 3),
                    a_time = TimeSpan.FromMinutes(20),
                };

                using (var context = new StormCI())
                {
                    context.entity_with_guid.Add(efEntity);
                    context.SaveChanges();


                    var sql = "select * from entity_with_guid where id = @id";
                    var parm = new[] { new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = efEntity.id } };
                    
                    var entity = MsSqlCi.Materialize<EntityWithGuid>(sql, parm, context.Database.Connection).First();
                }
            }
        }

        [TestMethod]
        public void ReadEntityWithGuid_AllNulls()
        {
            var efEntity = new entity_with_guid
            {
                id = Guid.NewGuid()
            };
        }
    }
}
