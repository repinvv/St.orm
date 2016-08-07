namespace StormCITest.Tests.DeleteTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class DeleteEnitityWithMultyKeyTests : ContextInTransaction
    {
        [TestMethod]
        public void Delete_EntityWithMultikey_Grouped()
        {
            Delete_EntityWithMultikey_Impl(EntityWithMultikeyCiService.MaxAmountForGroupedDelete);
        }

        [TestMethod]
        public void Delete_EntityWithMultikey_TempTable()
        {
            Delete_EntityWithMultikey_Impl(EntityWithMultikeyCiService.MaxAmountForGroupedDelete + 100);
        }

        private void Delete_EntityWithMultikey_Impl(int count)
        {
            // arrange
            var entities = Enumerable.Range(1, count * 2)
                                     .Select(Create.EntityWithMultikey)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toDelete = entities.Take(count).ToList();
            MsSqlCi.Delete(toDelete, conn);

            // assert
            var efEntities = context.entity_with_multikey.ToDictionary(x => (object)new { x.id_1, x.id_2 });
            foreach (var entity in entities.Except(toDelete))
            {
                efEntities.Should().ContainKey(Key(entity), "because entities except 'toDelete' should not be deleted");
            }

            foreach (var entity in toDelete)
            {
                efEntities.Should().NotContainKey(Key(entity), "because entities in 'toDelete' should be deleted");
            }
        }

        private object Key(EntityWithMultikey entity)
        {
            return new
                   {
                       id_1 = entity.Id1,
                       id_2 = entity.Id2
                   };
        }

        [TestMethod]
        public void Delete_EntityWithMultikey_GroupedVsTempTablePerf()
        {
            // arrange
            var amount = EntityWithMultikeyCiService.MaxAmountForGroupedDelete;
            var entities = Enumerable.Range(1, amount * 30)
                                     .Select(Create.EntityWithMultikey)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);
            var split1 = entities.SplitInGroupsBy(amount).ToList();
            var split2 = entities.SplitInGroupsBy(amount + 1).ToList();

            DeleteAll.EntityWithMultikey(conn);
            MsSqlCi.Insert(entities, conn);
            var time1 = WatchIt.Watch(() => split1.ForEach(x => MsSqlCi.Delete(x, conn)));

            DeleteAll.EntityWithMultikey(conn);
            MsSqlCi.Insert(entities, conn);
            var time2 = WatchIt.Watch(() => split2.ForEach(x => MsSqlCi.Delete(x, conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }
    }
}
