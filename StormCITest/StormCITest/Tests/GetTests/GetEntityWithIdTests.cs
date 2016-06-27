namespace StormCITest.Tests.GetTests
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    [TestClass]
    public class GetEntityWithIdTests : ContextInTransaction
    {
        [TestMethod]
        public void Get_EntityWithId_ManualQuery()
        {
            // arrange
            var efEntity = CreateFullEntity(1000);
            context.entity_with_id.Add(efEntity);
            context.SaveChanges();

            // act
            var sql = "select * from entity_with_id where id = @id";
            var parm = new[] { new SqlParameter("id", SqlDbType.Int) { Value = efEntity.id } };
            var entity = MsSqlCi
                .Get<EntityWithId>(sql, parm, conn)
                .First();

            // assert
            Compares.CompareEntity(efEntity, entity);
        }

        private entity_with_id CreateFullEntity(int intVal)
        {
            return new entity_with_id
                   {
                       a_bigint = 10L * 1000 * 1000 * 1000,
                       a_int = intVal,
                       a_numeric = 3123.22M, //precision is 6.2
                       a_bit = true,
                       a_smallint = 123,
                       a_decimal = 222.223M,
                       a_smallmoney = 333.334M,
                       a_tinyint = 22,
                       a_money = 444.44M
                   };
        }

        [TestMethod]
        public void Get_EntityWithId_ById()
        {
            // arrange
            var entities = Enumerable.Range(500, 3500).Select(CreateFullEntity).ToList();
            context.entity_with_id.AddRange(entities);
            context.SaveChanges();

            // act
            var ids = entities.Select(x => x.id).ToArray();
            var result = MsSqlCi.GetByPrimaryKey<EntityWithId>(ids, conn);
            var dict = result.ToDictionary(x => x.Id);

            // assert
            Assert.AreEqual(entities.Count, result.Count);
            foreach (var efEntity in entities)
            {
                Compares.CompareEntity(efEntity, dict[efEntity.id]);
            }
        }
    }
}
