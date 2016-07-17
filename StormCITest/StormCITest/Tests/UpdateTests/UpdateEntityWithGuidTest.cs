namespace StormCITest.Tests.UpdateTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class UpdateEntityWithGuidTest : ContextInTransaction
    {
        [TestMethod]
        public void Update_EntityWithGuid_Single()
        {
            // arrange
            var entity = Create.EntityWithGuid(100);
            MsSqlCi.Insert(entity, conn);

            // act
            var toUpdate = Create.EntityWithGuid(entity.Id);
            MsSqlCi.Update(toUpdate, conn);

            // assert
            var efEntities = context.entity_with_guid.ToList();
            efEntities.Count.Should().Be(1);
            Compare.EntityWithGuid(efEntities[0], toUpdate);
        }

        [TestMethod]
        public void Update_EntityWithGuid_Grouped()
        {
            Update_EntityWithGuid_Impl(EntityWithGuidCiService.MaxAmountForGroupedUpdate);
        }

        private void Update_EntityWithGuid_Impl(int length)
        {
            // arrange
            var entities = Enumerable.Range(10, length)
                                     .Select(Create.EntityWithGuid)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toUpdate = entities.Select(x => Create.EntityWithGuid(x.Id))
                                   .ToList();
            var time = WatchIt.Watch(() => MsSqlCi.Update(toUpdate, conn));
            Console.WriteLine(time);

            // assert
            var efEntities = context.entity_with_guid.ToDictionary(x => x.id);
            efEntities.Count.Should()
                      .Be(entities.Count);
            foreach (var entity in toUpdate)
            {
                Compare.EntityWithGuid(efEntities[entity.Id], entity);
            }
        }

        public void Update_EntityWithGuid_Bulk()
        {
            Update_EntityWithGuid_Impl(EntityWithGuidCiService.MaxAmountForGroupedUpdate + 100);
        }

        [TestMethod]
        public void Update_EnitityWithGuid_BulkVsGroupPerf()
        {
            var amount = EntityWithGuidCiService.MaxAmountForGroupedUpdate;
            var entities = Enumerable.Range(10, amount * (amount + 1))
                                     .Select(Create.EntityWithGuid)
                                     .ToList();
            var toUpdate = entities.Select(x => Create.EntityWithGuid(x.Id))
                                   .ToList();
            var split1 = toUpdate.SplitInGroupsBy(amount).ToList();
            var split2 = toUpdate.SplitInGroupsBy(amount + 1).ToList();

            MsSqlCi.Insert(entities, conn);
            var time1 = WatchIt.Watch(() => split1.ForEach(x => MsSqlCi.Update(x, conn)));
            DeleteAll.EntityWithGuid(conn);
            MsSqlCi.Insert(entities, conn);
            var time2 = WatchIt.Watch(() => split2.ForEach(x => MsSqlCi.Update(x, conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }
    }
}
