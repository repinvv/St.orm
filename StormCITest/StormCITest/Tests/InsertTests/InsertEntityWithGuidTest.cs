namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class InsertEntityWithGuidTest : ContextInTransaction
    {
        [TestMethod]
        public void Insert_EnitityWithGuid_Single()
        {
            // arrange
            var entity = CreateFullEntity(1000);

            // act
            MsSqlCi.Insert(new List<EntityWithGuid> { entity }, conn);

            // assert
            var efEntity = context.entity_with_guid.First(x => x.id == entity.Id);
            Compares.CompareEntity(efEntity, entity);
        }

        private EntityWithGuid CreateFullEntity(int i)
        {
            return new EntityWithGuid
                   {
                       Id = Guid.NewGuid(),
                       ADate = new DateTime(2016, 1, 1),
                       ADatetime = new DateTime(2016, 1, 2, 1, 2, 3),
                       ADatetime2 = new DateTime(2016, 1, 3, 1, 2, 3),
                       AFloat = 123.456,
                       AOffset = new DateTimeOffset(2016, 1, 4, 1, 2, 3, TimeSpan.FromMinutes(10)),
                       AReal = (float)234.567,
                       ASmalldatetime = new DateTime(2016, 1, 5, 1, 2, 0), //precision is one minute
                       ATime = TimeSpan.FromMinutes(20),
                   };
        }

        public void Insert_EnitityWithId_TwoHundred()
        {
            // arrange
            var entities = Enumerable.Range(10, 200).Select(CreateFullEntity).ToList();

            // act
            MsSqlCi.Insert(entities, conn);

            // assert
            var efEntities = context.entity_with_guid.ToDictionary(x => x.id);
            foreach (var entity in entities)
            {
                Compares.CompareEntity(efEntities[entity.Id], entity);
            }
        }

        [TestMethod]
        public void Insert_EnitityWithGuid_Perf()
        {
            // arrange
            var entities = Enumerable.Range(10, 200000).Select(CreateFullEntity).ToList();
            // About a second on my system, if all tests are executed, i.e. ado dlls are loaded and heated

            // act
            var time = WatchIt.Watch(() => MsSqlCi.Insert(entities, conn));
            Console.WriteLine(time);
        }
    }
}
