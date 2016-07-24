namespace StormCITest.Tests.GetTests
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    [TestClass]
    public class GetEntityWithGuidTests : ContextInTransaction
    {
        [TestMethod]
        public void Get_EntityWithGuid_AllEntries()
        {
            // arrange
            var efEntity = CreateFullEntity();
            context.entity_with_guid.Add(efEntity);
            context.SaveChanges();

            // act
            var sql = "select * from entity_with_guid where id = @id";
            var parm = new[] { new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = efEntity.id } };
            var entity = MsSqlCi
                .Get<EntityWithGuid>(sql, parm, conn)
                .First();

            // assert
            Compare.EntityWithGuid(efEntity, entity);
        }

        private static entity_with_guid CreateFullEntity()
        {
            var efEntity = new entity_with_guid
            {
                id = Guid.NewGuid(),
                a_date = new DateTime(2016, 1, 1),
                a_datetime = new DateTime(2016, 1, 2, 1, 2, 3),
                a_datetime2 = new DateTime(2016, 1, 3, 1, 2, 3),
                a_float = 123.456,
                a_offset = new DateTimeOffset(2016, 1, 4, 1, 2, 3, TimeSpan.FromMinutes(10)),
                a_real = (float)234.567,
                a_smalldatetime = new DateTime(2016, 1, 5, 1, 2, 0), //precision is one minute
                a_time = TimeSpan.FromMinutes(20),
            };
            return efEntity;
        }

        [TestMethod]
        public void Get_EntityWithGuid_AllNulls()
        {
            // arrange
            var efEntity = new entity_with_guid { id = Guid.NewGuid(), a_real = (float)234.567, };
            context.entity_with_guid.Add(efEntity);
            context.SaveChanges();

            // act
            var sql = "select * from entity_with_guid where id = @id";
            var parm = new[] { new SqlParameter("id", SqlDbType.UniqueIdentifier) { Value = efEntity.id } };
            var entity = MsSqlCi
                .Get<EntityWithGuid>(sql, parm, conn)
                .First();

            // assert
            CompareEmptyEntity(efEntity, entity);

        }

        private void CompareEmptyEntity(entity_with_guid efEntity, EntityWithGuid entity)
        {
            Assert.AreEqual(efEntity.id, entity.Id);
            Assert.IsTrue(efEntity.a_real == entity.AReal);
            Assert.IsNull(entity.ADate);
            Assert.IsNull(entity.ADatetime);
            Assert.IsNull(entity.ADatetime2);
            Assert.IsNull(entity.AFloat);
            Assert.IsNull(entity.AOffset);
            Assert.IsNull(entity.ASmalldatetime);
            Assert.IsNull(entity.ATime);
        }

        [TestMethod]
        public void Get_EntityWithGuid_ByIdByTempTable()
        {
            GetEntityWithGuidByIdImpl(EntityWithGuidCiService.MaxAmountForWhereIn + 1000);
        }

        [TestMethod]
        public void Get_EntityWithGuid_ByIdByWhereIn()
        {
            GetEntityWithGuidByIdImpl(EntityWithGuidCiService.MaxAmountForWhereIn - 5);
        }

        private void GetEntityWithGuidByIdImpl(int length)
        {
            // arrange
            var entities = Enumerable.Range(500, length)
                                     .Select(Create.EntityWithGuid)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var ids = entities.Select(x => x.Id)
                              .ToArray();

            List<EntityWithGuid> result = MsSqlCi.GetByPrimaryKey<EntityWithGuid>(ids, conn);

            var dict = result.ToDictionary(x => x.Id);

            // assert
            Assert.AreEqual(entities.Count, result.Count);
            foreach (var src in entities)
            {
                Compare.EntityWithGuid(src, dict[src.Id]);
            }
        }

        [TestMethod]
        public void Get_EntityWithGuid_WhereInVsTempTablePerf()
        {
            var amount = EntityWithGuidCiService.MaxAmountForWhereIn;
            var entities = Enumerable.Range(500, 10000)
                                     .Select(Create.EntityWithGuid)
                                     .ToList();

            var split1 = entities.Select(x => x.Id).SplitInGroupsBy(amount).ToList();
            var split2 = entities.Select(x => x.Id).SplitInGroupsBy(amount + 1).ToList();

            var time1 = WatchIt
                .Watch(() => split1.ForEach(x => MsSqlCi.GetByPrimaryKey<EntityWithGuid>(x.ToArray(), conn)));
            var time2 = WatchIt
                .Watch(() => split2.ForEach(x => MsSqlCi.GetByPrimaryKey<EntityWithGuid>(x.ToArray(), conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }
    }
}
