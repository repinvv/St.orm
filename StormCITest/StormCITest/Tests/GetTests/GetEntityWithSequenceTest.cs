namespace StormCITest.Tests.GetTests
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;
    using StormTestProject.StormSchema.SomeSchema;

    [TestClass]
    public class GetEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Get_EntityWithSequence()
        {
            // arrange
            var src = Create.EntityWithSequence(100);
            MsSqlCi.Insert(src, conn);

            // act
            var sql = "select * from some_schema.entity_with_sequence where id = @id";
            var parm = new[] { new SqlParameter("id", SqlDbType.Int) { Value = src.Id } };
            var entity = MsSqlCi
                .Get<EntityWithSequence>(sql, parm, conn)
                .First();

            // assert
            Compare.EntityWithSequence(src, entity);
        }

        [TestMethod]
        public void Get_EntityWithSequence_ById()
        {
            // arrange
            var src = Enumerable.Range(100,1000).Select(Create.EntityWithSequence).ToList();
            MsSqlCi.Insert(src, conn);

            // act
            var ids = src.Select(x => x.Id).ToArray();
            var entities = MsSqlCi.GetByPrimaryKey<EntityWithSequence>(ids, conn);
            var dict = entities.ToDictionary(x => x.Id);

            // assert
            Assert.AreEqual(src.Count, entities.Count);
            foreach (var entity in src)
            {
                Compare.EntityWithSequence(entity, dict[entity.Id]);
            }
            
        }
    }
}
