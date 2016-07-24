namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormSchema;

    [TestClass]
    public class InsertSmallEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Insert_SmallEnitityWithSequence_Grouped()
        {
            Insert_SmallEnitityWithSequence_Impl(SmallentityWithSequenceCiService.MaxAmountForGroupedInsert - 3);
        }

        [TestMethod]
        public void Insert_SmallEnitityWithSequence_Bulk()
        {
            Insert_SmallEnitityWithSequence_Impl(SmallentityWithSequenceCiService.MaxAmountForGroupedInsert + 3);
        }

        private void Insert_SmallEnitityWithSequence_Impl(int length)
        {
            // arrange
            var entities = Enumerable.Range(1, length)
                                     .Select(Create.SmallEntityWithSequence)
                                     .ToList();

            // act
            MsSqlCi.Insert(entities, conn);

            // assert
            var efEntities = context.smallentity_with_sequence.ToDictionary(x => x.id);
            entities.ForEach(x => Compare.SmallEntityWithSequence(efEntities[x.Id], x));
        }

        [TestMethod]
        public void Insert_SmallEnitityWithSequence_Perf()
        {
            var amount = SmallentityWithSequenceCiService.MaxAmountForGroupedInsert;
            var entities = Enumerable.Range(10, amount * (amount + 1) * 5)
                                     .Select(x => Create.SmallEntityWithSequence(100))
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
                var sql = "delete from smallentity_with_sequence";
                CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], (SqlConnection)conn, null);
            }
        }
    }
}
