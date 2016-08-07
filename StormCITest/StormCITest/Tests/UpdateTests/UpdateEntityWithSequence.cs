namespace StormCITest.Tests.UpdateTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class UpdateEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Update_EntityWithSequence_Single()
        {
            // arrange
            var entity = Create.EntityWithSequence(100);
            MsSqlCi.Insert(entity, conn);

            // act
            var toUpdate = Create.EntityWithSequenceForUpdate(entity.Id);
            MsSqlCi.Update(toUpdate, conn);

            // assert
            var efEntity = context.entity_with_sequence.First(x=>x.id == toUpdate.Id);
            Compare.EntityWithSequence(efEntity, toUpdate);
        }

        [TestMethod]
        public void Update_EntityWithSequence_Grouped()
        {
            Update_EntityWithSequence_Impl(EntityWithSequenceCiService.MaxAmountForGroupedUpdate);
        }

        [TestMethod]
        public void Update_EntityWithSequence_Bulk()
        {
            Update_EntityWithSequence_Impl(EntityWithSequenceCiService.MaxAmountForGroupedUpdate + 10);
        }

        private void Update_EntityWithSequence_Impl(int length)
        {
            // arrange
            var entities = Enumerable.Range(10, length)
                                     .Select(Create.EntityWithSequence)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toUpdate = entities.Select(x => Create.EntityWithSequenceForUpdate(x.Id))
                                   .ToList();
            var time = WatchIt.Watch(() => MsSqlCi.Update(toUpdate, conn));
            Console.WriteLine(time);

            // assert
            var efEntities = context.entity_with_sequence.ToDictionary(x => x.id);
            efEntities.Count.Should()
                      .Be(entities.Count);
            foreach (var entity in toUpdate)
            {
                Compare.EntityWithSequence(efEntities[entity.Id], entity);
            }
        }
    }
}
