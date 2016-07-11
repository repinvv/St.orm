namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;
    using StormTestProject.StormSchema.SomeSchema;

    [TestClass]
    public class InsertEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Insert_EnitityWithSequence_Single()
        {
            // arrange
            var entity = Create.EntityWithSequence(1000);

            // act
            MsSqlCi.Insert(entity, conn);

            // assert
            var efEntity = context.entity_with_sequence.First(x => x.id == entity.Id);
            Compare.EntityWithSequence(efEntity, entity);
        }


        [TestMethod]
        public void Insert_EnitityWithSequence_Grouped()
        {
            Insert_EnitityWithSequence_Impl(EntityWithSequenceCiService.MaxAmountForGroupedInsert - 10);
        }

        private void Insert_EnitityWithSequence_Impl(int length)
        {
            // arrange
            var entities = Enumerable.Range(1, length)
                                     .Select(Create.EntityWithSequence)
                                     .ToList();

            // act
            MsSqlCi.Insert(entities, conn);

            // assert
            var efEntities = context.entity_with_sequence.ToDictionary(x => x.id);
            entities.ForEach(x => Compare.EntityWithSequence(efEntities[x.Id], x));
        }

        [TestMethod]
        public void Insert_EnitityWithSequence_Bulk()
        {
            Insert_EnitityWithSequence_Impl(EntityWithSequenceCiService.MaxAmountForGroupedInsert + 20);
        }

        [TestMethod]
        public void Insert_EnitityWithSequence_Perf()
        {
            var amount = EntityWithSequenceCiService.MaxAmountForGroupedInsert;
            var entities = Enumerable.Range(10, amount * (amount + 1) * 30)
                                     .Select(x => Create.EntityWithSequence(100))
                                     .ToList();

            var split1 = entities.SplitInGroupsBy(amount).ToList();
            var split2 = entities.SplitInGroupsBy(amount + 1).ToList();

            var time1 = WatchIt.Watch(() => split1.ForEach(x => MsSqlCi.Insert(x, conn)));
            DeleteAll();
            var time2 = WatchIt.Watch(() => split2.ForEach(x => MsSqlCi.Insert(x, conn)));

            Console.WriteLine(time1);
            Console.WriteLine(time2);
        }

        private void DeleteAll()
        {
            using (new ConnectionHandler(conn))
            {
                var sql = "delete from some_schema.entity_with_sequence";
                CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], (SqlConnection)conn, null);
            }
        }
    }
}
