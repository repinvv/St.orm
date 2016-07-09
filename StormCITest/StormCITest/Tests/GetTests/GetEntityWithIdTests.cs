namespace StormCITest.Tests.GetTests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    [TestClass]
    public class GetEntityWithIdTests : ContextInTransaction
    {
        [TestMethod]
        public void Get_EntityWithId_ManualQuery()
        {
            // arrange
            var efEntity = Create.EfEntityWithId(1000);
            context.entity_with_id.Add(efEntity);
            context.SaveChanges();

            // act
            var sql = "select * from entity_with_id where id = @id";
            var parm = new[] { new SqlParameter("id", SqlDbType.Int) { Value = efEntity.id } };
            var entity = MsSqlCi
                .Get<EntityWithId>(sql, parm, conn)
                .First();

            // assert
            Compare.EntityWithId(efEntity, entity);
        }

        [TestMethod]
        public void Get_EntityWithId_ByIdByTempTable()
        {
            GetEntityWithIdByIdImpl(1000);
        }

        [TestMethod]
        public void Get_EntityWithId_ByIdByWhereIn()
        {
            GetEntityWithIdByIdImpl(30);
        }

        private void GetEntityWithIdByIdImpl(int length)
        {
            // arrange
            var entities = Enumerable.Range(500, length)
                                     .Select(Create.EntityWithId)
                                     .ToList();
            MsSqlCi.Insert(entities, conn);

            // act
            var ids = entities.Select(x => x.Id)
                              .ToArray();

            List<EntityWithId> result = null;
            var time = WatchIt.Watch(() => { result = MsSqlCi.GetByPrimaryKey<EntityWithId>(ids, conn); });
            Console.WriteLine(time);
            var dict = result.ToDictionary(x => x.Id);

            // assert
            Assert.AreEqual(entities.Count, result.Count);
            foreach (var src in entities)
            {
                Compare.EntityWithId(src, dict[src.Id]);
            }
        }
    }
}
