namespace StormCITest.Tests.UpdateTests
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class UpdateEntityWithIdTest : ContextInTransaction
    {
        [TestMethod]
        public void Update_EntityWithId_Grouped()
        {
            Update_EntityWithId_Impl(EntityWithIdCiService.MaxAmountForGroupedUpdate);
        }

        [TestMethod]
        public void Update_EntityWithId_Bulk()
        {
            Update_EntityWithId_Impl(EntityWithIdCiService.MaxAmountForGroupedUpdate + 10);
        }

        private void Update_EntityWithId_Impl(int length)
        {
            // arrange
            var entities = Enumerable.Range(10, length)
                                     .Select(Create.EntityWithId)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var toUpdate = entities.Select(x => Create.EntityWithIdForUpdate(x.Id))
                                   .ToList();
            var time = WatchIt.Watch(() => MsSqlCi.Update(toUpdate, conn));
            Console.WriteLine(time);

            // assert
            var efEntities = context.entity_with_id.ToDictionary(x => x.id);
            efEntities.Count.Should()
                      .Be(entities.Count);
            foreach (var entity in toUpdate)
            {
                Compare.EntityWithId(efEntities[entity.Id], entity);
            }
        }
    }
}
