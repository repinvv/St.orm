namespace StormCITest.Tests.ReadTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    [TestClass]
    internal class ReadEntityWithMultikeyTest : ContextInTransaction
    {
        [TestMethod]
        public void Read_EntityWithMultiKey_ByIdsTuple()
        {
            // arrange
            var entities = Enumerable.Range(500, 3500).Select(CreateEntity).ToList();
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
                CompareEntity(efEntity, dict[new { id1 = efEntity.id_1, id2 = efEntity.id_2 }]);
            }
        }

        private void CompareEntity(entity_with_multikey efEntity, EntityWithMultikey entity)
        {
            Assert.AreEqual(efEntity.id_1, entity.Id1);
            Assert.AreEqual(efEntity.id_2, entity.Id2);
            Assert.AreEqual(efEntity.content, entity.Content);
        }

        private entity_with_multikey CreateEntity(int i)
        {
            return new entity_with_multikey
                   {
                       id_1 = i,
                       id_2 = "id2",
                       content = "content " + i
                   };
        }
    }
}
