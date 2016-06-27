﻿namespace StormCITest.Tests.GetTests
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
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
            CompareFullEntity(efEntity, entity);


        }

        private void CompareFullEntity(entity_with_guid efEntity, EntityWithGuid entity)
        {
            Assert.AreEqual(efEntity.id, entity.Id);
            Assert.IsTrue(efEntity.a_real == entity.AReal);
            Assert.AreEqual(efEntity.a_date, entity.ADate);
            Assert.AreEqual(efEntity.a_datetime, entity.ADatetime);
            Assert.AreEqual(efEntity.a_datetime2, entity.ADatetime2);
            Assert.AreEqual(efEntity.a_float, entity.AFloat);
            Assert.AreEqual(efEntity.a_offset, entity.AOffset);
            Assert.AreEqual(efEntity.a_smalldatetime, entity.ASmalldatetime);
            Assert.AreEqual(efEntity.a_time, entity.ATime);
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
    }
}