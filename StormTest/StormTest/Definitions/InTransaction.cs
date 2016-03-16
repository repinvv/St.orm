using System.Data.SqlClient;
using System.Transactions;
using StormTest.Schema;
using TechTalk.SpecFlow;
using TestingContextCore.PublicMembers;

namespace StormTest.Definitions
{
    using IsolationLevel = System.Data.IsolationLevel;

    [Binding]
    public class InTransaction
    {
        private readonly Storage storage;

        public InTransaction(Storage storage)
        {
            this.storage = storage;
        }

        [BeforeScenario("inTransaction", Order = 10)]
        public void StartTransaction()
        {
            
            var transaction = new TransactionScope(TransactionScopeOption.Required);
            var context = new StormTestDB();
            storage.Set(transaction);
            storage.Set(context);
        }

        [AfterScenario("inTransaction")]
        public void AbortTransaction()
        {
            storage.Get<TransactionScope>().Dispose();
            var context = storage.Get<StormTestDB>();
            context.Dispose();
        }
    }
}
