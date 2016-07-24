namespace StormCITest.Tests.UpdateTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class UpdateSmallEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Update_SmallentityWithSequence_Grouped()
        {
            Update_SmallentityWithSequence_Impl(SmallentityWithSequenceCiService.MaxAmountForGroupedUpdate);
        }

        [TestMethod]
        public void Update_SmallentityWithSequence_Bulk()
        {
            Update_SmallentityWithSequence_Impl(SmallentityWithSequenceCiService.MaxAmountForGroupedUpdate + 100);
        }

        private void Update_SmallentityWithSequence_Impl(int length)
        {
            // arrange
            var entities = Enumerable.Range(10, length)
                                     .Select(Create.SmallEntityWithSequence)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toUpdate = entities.Select(x => Create.SmallEntityWithSequenceForUpdate(x.Id))
                                   .ToList();
            var time = WatchIt.Watch(() => MsSqlCi.Update(toUpdate, conn));
            Console.WriteLine(time);

            // assert
            var efEntities = context.smallentity_with_sequence.ToDictionary(x => x.id);
            efEntities.Count.Should()
                      .Be(entities.Count);
            foreach (var entity in toUpdate)
            {
                Compare.SmallEntityWithSequence(efEntities[entity.Id], entity);
            }
        }

        [TestMethod]
        public void Update_SmallentityWithSequence_BulkVsGroupPerf()
        {
            var amount = SmallentityWithSequenceCiService.MaxAmountForGroupedUpdate;
            var entities = Enumerable.Range(10, amount * (amount + 1))
                                     .Select(Create.SmallEntityWithSequence)
                                     .ToList();
            var toUpdate = entities.Select(x => Create.SmallEntityWithSequenceForUpdate(x.Id))
                                   .ToList();
            var split1 = toUpdate.SplitInGroupsBy(amount).ToList();
            var split2 = toUpdate.SplitInGroupsBy(amount + 1).ToList();

            MsSqlCi.Insert(entities, conn);
            var time1 = WatchIt.Watch(() => split1.Portion(4).ForEach(x => MsSqlCi.Update(x, conn)));
            DeleteAll.EntityWithGuid(conn);
            MsSqlCi.Insert(entities, conn);
            var time2 = WatchIt.Watch(() => split2.Portion(4).ForEach(x => MsSqlCi.Update(x, conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }
    }
}
