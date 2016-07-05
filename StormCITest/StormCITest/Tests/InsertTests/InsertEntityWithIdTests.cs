namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class InsertEntityWithIdTests : ContextInTransaction
    {
        [TestMethod]
        public void Insert_EnitityWithId_Single()
        {
            // arrange
            var entity = Create.EntityWithId(1000);

            // act
            MsSqlCi.Insert(entity, conn);

            // assert
            Assert.IsTrue(entity.Id > 0);
            var efEntity = context.entity_with_id.First(x => x.id == entity.Id);
            Compare.EntityWithId(efEntity, entity);
        }



        [TestMethod]
        public void Insert_EnitityWithId_TwoHundred()
        {
            // arrange
            var entities = Enumerable.Range(10, 200).Select(Create.EntityWithId).ToList();

            // act
            MsSqlCi.Insert(entities, conn);

            // assert
            Assert.IsTrue(entities.All(x => x.Id > 0));
            var minId = entities.Min(x => x.Id);
            var maxId = entities.Max(x => x.Id);
            var efEntities = context.entity_with_id.Where(x => x.id >= minId)
                                  .Where(x => x.id <= maxId)
                                  .ToDictionary(x => x.id);
            foreach (var entity in entities)
            {
                Compare.EntityWithId(efEntities[entity.Id], entity);
            }
        }

        [TestMethod]
        public void Insert_EnitityWithId_Perf()
        {
            // arrange
            var entities = Enumerable.Range(10, 3000).Select(Create.EntityWithId).ToList(); 
            // About a second on my system

            // act
            var time = WatchIt.Watch(() => MsSqlCi.Insert(entities, conn));
            Console.WriteLine(time);
        }
    }
}
