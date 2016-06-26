namespace StormCITest.Tests
{
    using System.Data.Common;
    using System.Transactions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;

    public class ContextInTransaction
    {
        private TransactionScope trans;
        protected StormCI context;
        protected DbConnection conn;

        [TestInitialize]
        public void StartTransaction()
        {
            this.trans = new TransactionScope(TransactionScopeOption.Required);
            this.context = new StormCI();
            this.conn = context.Database.Connection;
        }

        [TestCleanup]
        public void RollbackTransaction()
        {
            context.Dispose();
            trans.Dispose();
        }
    }
}
