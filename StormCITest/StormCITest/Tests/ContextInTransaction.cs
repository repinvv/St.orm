namespace StormCITest.Tests
{
    using System.Transactions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;

    public class ContextInTransaction
    {
        private TransactionScope trans;
        protected StormCI context;

        [TestInitialize]
        public void StartTransaction()
        {
            this.trans = new TransactionScope(TransactionScopeOption.Required);
            this.context = new StormCI();
        }

        [TestCleanup]
        public void RollbackTransaction()
        {
            context.Dispose();
            trans.Dispose();
        }
    }
}
