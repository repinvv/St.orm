namespace StormCITest.Tests.GetTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    [TestClass]
    public class GetEntityWithMultikeyTest : ContextInTransaction
    {
        [TestMethod]
        public void Get_EntityWithMultiKey_ByIds()
        {
            // arrange
            var entities = Enumerable.Range(500, 2000).Select(Create.EfEntityWithMultikey).ToList();
            context.entity_with_multikey.AddRange(entities);
            context.SaveChanges();

            // act
            
            var id1 = entities.Select(x => x.id_1).ToArray();
            var id2 = entities.Select(x => x.id_2).ToArray();
            var keys = new object[] { id1, id2 };
            

            var result = MsSqlCi.GetByPrimaryKey<EntityWithMultikey>(keys, conn);
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
