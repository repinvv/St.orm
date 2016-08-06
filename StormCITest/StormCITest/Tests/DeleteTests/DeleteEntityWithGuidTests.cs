namespace StormCITest.Tests.DeleteTests
{
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class DeleteEntityWithGuidTests : ContextInTransaction
    {
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
    }
}
