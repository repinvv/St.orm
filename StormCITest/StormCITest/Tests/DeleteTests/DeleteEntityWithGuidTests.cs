namespace StormCITest.Tests.DeleteTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class DeleteEntityWithGuidTests : ContextInTransaction
    {
        [TestMethod]
        public void Delete_EntityWithGuid_Single()
        {
            // arrange
            var entities = Enumerable.Range(1, 2)
                                     .Select(Create.EntityWithGuid)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toDelete = entities.First();
            MsSqlCi.Delete(toDelete, conn);

            // assert
            var efEntities = context.entity_with_guid.ToDictionary(x => x.id);
            efEntities.Should().ContainKey(entities.Last().Id, "because entities except 'toDelete' should not be deleted");

            efEntities.Should().NotContainKey(toDelete.Id, "because entities in 'toDelete' should be deleted");
            
        }

        [TestMethod]
        public void Delete_EntityWithGuid_WhereIn()
        {
            Delete_EntityWithGuid_Impl(EntityWithGuidCiService.MaxAmountForWhereIn);
        }

        [TestMethod]
        public void Delete_EntityWithGuid_TempTable()
        {
            Delete_EntityWithGuid_Impl(EntityWithGuidCiService.MaxAmountForWhereIn + 100);
        }

        private void Delete_EntityWithGuid_Impl(int count)
        {
            // arrange
            var entities = Enumerable.Range(1, count * 2)
                                     .Select(Create.EntityWithGuid)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toDelete = entities.Take(count).ToList();
            MsSqlCi.Delete(toDelete, conn);

            // assert
            var efEntities = context.entity_with_guid.ToDictionary(x => x.id);
            foreach (var entity in entities.Except(toDelete))
            {
                efEntities.Should().ContainKey(entity.Id, "because entities except 'toDelete' should not be deleted");
            }

            foreach (var entity in toDelete)
            {
                efEntities.Should().NotContainKey(entity.Id, "because entities in 'toDelete' should be deleted");
            }
        }

        [TestMethod]
        public void Delete_EnitityWithGuid_WhereInVsTempTablePerf()
        {
            var amount = EntityWithGuidCiService.MaxAmountForWhereIn;
            var entities = Enumerable.Range(10, amount * 100)
                                     .Select(Create.EntityWithGuid)
                                     .ToList();

            var split1 = entities.SplitInGroupsBy(amount).ToList();
            var split2 = entities.SplitInGroupsBy(amount + 1).ToList();

            DeleteAll.EntityWithGuid(conn);
            MsSqlCi.Insert(entities, conn);
            var time1 = WatchIt.Watch(() => split1.ForEach(x => MsSqlCi.Delete(x, conn)));

            DeleteAll.EntityWithGuid(conn);
            MsSqlCi.Insert(entities, conn);
            var time2 = WatchIt.Watch(() => split2.ForEach(x => MsSqlCi.Delete(x, conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }
    }
}
