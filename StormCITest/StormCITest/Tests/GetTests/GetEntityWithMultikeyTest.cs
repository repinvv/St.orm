namespace StormCITest.Tests.GetTests
{
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    [TestClass]
    public class GetEntityWithMultikeyTest : ContextInTransaction
    {
        [TestMethod]
        public void Get_EntityWithMultiKey()
        {
            // arrange
            var entities = Enumerable.Range(500, 2000).Select(Create.EfEntityWithMultikey).ToList();
            context.entity_with_multikey.AddRange(entities);
            context.SaveChanges();

            // act
            var sql = "select * from entity_with_multikey";
            var result = MsSqlCi.Get<EntityWithMultikey>(sql, new SqlParameter[0], conn);
            var dict = result.ToDictionary(x => new { id1 = x.Id1, id2 = x.Id2 });

            // assert
            Assert.AreEqual(entities.Count, result.Count);
            foreach (var efEntity in entities)
            {
                Compare.EntityWithMultikey(efEntity, dict[new { id1 = efEntity.id_1, id2 = efEntity.id_2 }]);
            }
        }
    }
}
