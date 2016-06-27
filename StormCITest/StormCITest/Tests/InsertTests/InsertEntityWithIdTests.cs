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
            var entity = CreateFullEntity(1000);

            // act
            MsSqlCi.Insert(new List<EntityWithId> { entity }, conn);

            // assert
            Assert.IsTrue(entity.Id > 0);
            var efEntity = context.entity_with_id.First(x => x.id == entity.Id);
            Compares.CompareEntity(efEntity, entity);
        }

        private EntityWithId CreateFullEntity(int intVal)
        {
            return new EntityWithId()
                   {
                       ABigint = 10L * 1000 * 1000 * 1000,
                       AInt = intVal,
                       ANumeric = 3123.22M, //precision is 6.2
                       ABit = true,
                       ASmallint = 123,
                       ADecimal = 222.223M,
                       ASmallmoney = 333.334M,
                       ATinyint = 22,
                       AMoney = 444.44M
                   };
        }

        [TestMethod]
        public void Insert_EnitityWithId_TwoHundred()
        {
            // arrange
            var entities = Enumerable.Range(10, 200).Select(CreateFullEntity).ToList();

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
                Compares.CompareEntity(efEntities[entity.Id], entity);
            }
        }

        [TestMethod]
        public void Insert_EnitityWithId_Perf()
        {
            // arrange
            var entities = Enumerable.Range(10, 3010).Select(CreateFullEntity).ToList(); 
            // About a second on my system

            // act
            var time = WatchIt.Watch(() => MsSqlCi.Insert(entities, conn));
            Console.WriteLine(time);
        }
    }
}
