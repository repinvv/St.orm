namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
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

        [TestMethod]
        public void Insert_EnitityWithGuid_SinglePerf()
        {
            var entities = Enumerable.Range(10, 1000).Select(CreateFullEntity).ToList();

            var time1 = WatchIt.Watch(() => entities.ForEach(x => MsSqlCi.Insert(new List<EntityWithGuid> { x }, conn)));
            DeleteAll();
            var time2 = WatchIt.Watch(() => entities.ForEach(x => SingleInsert(x)));
            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }

        private void DeleteAll()
        {
            using (new ConnectionHandler(conn))
            {
                var sql = "delete from entity_with_guid";
                CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], (SqlConnection)conn, null);
            }
        }

        private void SingleInsert(EntityWithGuid e)
        {
            using (new ConnectionHandler(conn))
            {
                var sql = @"insert into entity_with_guid (id, a_float, a_real, a_date, a_time,
                a_offset, a_datetime, a_datetime2, a_smalldatetime) values (@id, @a_float, @a_real, @a_date, @a_time,
                @a_offset, @a_datetime, @a_datetime2, @a_smalldatetime)";
                var pams = new[]
                           {
                               new SqlParameter("id", e.Id),
                               new SqlParameter("a_float", e.AFloat),
                               new SqlParameter("a_real", e.AReal),
                               new SqlParameter("a_date", e.ADate),
                               new SqlParameter("a_time", e.ATime),
                               new SqlParameter("a_offset", e.AOffset),
                               new SqlParameter("a_datetime", e.ADatetime),
                               new SqlParameter("a_datetime2", e.ADatetime2),
                               new SqlParameter("a_smalldatetime", e.ASmalldatetime),
                           };
                CiHelper.ExecuteNonQuery(sql, pams, (SqlConnection)conn, null);
            }
        }
    }
}
