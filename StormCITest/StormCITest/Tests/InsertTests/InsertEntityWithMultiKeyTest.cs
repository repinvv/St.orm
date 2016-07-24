namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class InsertEntityWithMultiKeyTest : ContextInTransaction
    {
        [TestMethod]
        public void Insert_EnitityWithMultikey_Bulk()
        {
            InsertEnitityWithMultikeyImpl(EntityWithMultikeyCiService.MaxAmountForGroupedInsert + 200);
        }

        [TestMethod]
        public void Insert_EnitityWithMultikey_Grouped()
        {
            InsertEnitityWithMultikeyImpl(EntityWithMultikeyCiService.MaxAmountForGroupedInsert - 3);
        }

        private void InsertEnitityWithMultikeyImpl(int length)
        {
            // arrange
            var entities = Enumerable.Range(10, length)
                                     .Select(Create.EntityWithMultikey)
                                     .ToList();

            // act
            MsSqlCi.Insert(entities, conn);

            // assert
            var efEntities = context.entity_with_multikey.ToDictionary(x => new { x.id_1, x.id_2 });
            foreach (var entity in entities)
            {
                Compare.EntityWithMultikey(efEntities[new {id_1 = entity.Id1, id_2 = entity.Id2}], entity);
            }
        }

        [TestMethod]
        public void Insert_EnitityWithMultiKey_BulkVsGroupPerf()
        {
            var amount = EntityWithMultikeyCiService.MaxAmountForGroupedInsert;
            var entities = Enumerable.Range(10, amount * (amount + 1) * 5)
                                     .Select(Create.EntityWithMultikey)
                                     .ToList();

            var split1 = entities.SplitInGroupsBy(amount).ToList();
            var split2 = entities.SplitInGroupsBy(amount + 1).ToList();

            var time1 = WatchIt.Watch(() => split1.ForEach(x => MsSqlCi.Insert(x, conn)));
            DeleteAll.EntityWithMultikey(conn);
            var time2 = WatchIt.Watch(() => split2.ForEach(x => MsSqlCi.Insert(x, conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }
    }
}
