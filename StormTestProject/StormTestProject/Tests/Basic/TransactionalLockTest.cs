namespace StormTestProject.Tests.Basic
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using EntityFramework.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormModel;
    using StormTestProject.Tests.Helpers;
    using StormTestProject.Tests.Infrastructure;

    [TestClass]
    public class TransactionalLockTest
    {
        private class ThreadParam
        {
            public int TaxIndex { get; set; }

            public DateTime? LockDate { get; set; }
        }

        private Policy policy;

        private int lockLength = 200;

        [TestInitialize]
        public void Prepare()
        {
            policy = TestConfig.Get<PolicyHelper>()
                               .CreatePolicyWith2Taxes();
            TestConfig.Get<StormTestContext>().Storm.Save(policy);
        }

        [TestCleanup]
        public void Remove()
        {
            var context = new StormTestContext();
            var indexes = policy.Taxes.Select(x => x.TaxId)
                                .ToList();
            context.Taxes
                   .Where(x => indexes.Contains(x.TaxId))
                   .Delete();
            context.Policies.Where(x => x.PolicyId == policy.PolicyId)
                   .Delete();
        }

        [TestMethod]
        public void TaxLock_BlocksReadingSameTax()
        {
            var tp1 = new ThreadParam { TaxIndex = 1 };
            var tp2 = new ThreadParam { TaxIndex = 1 };
            var thread1 = new Thread(LockTest);
            var thread2 = new Thread(LockTest);
            Debug.WriteLine(DateTime.Now.Millisecond);
            thread1.Start(tp1);
            while (tp1.LockDate == null) { Thread.Sleep(1); }
            Debug.WriteLine(DateTime.Now.Millisecond);
            thread2.Start(tp2);
            while (tp2.LockDate == null) { Thread.Sleep(1); }
            Debug.WriteLine(DateTime.Now.Millisecond);
            Assert.IsTrue(tp1.LockDate.Value.AddMilliseconds(lockLength) < tp2.LockDate);
        }

        [TestMethod]
        public void TaxLock_AllowsReadingAnotherTax()
        {
            var tp1 = new ThreadParam { TaxIndex = 1 };
            var tp2 = new ThreadParam { TaxIndex = 0 };
            var thread1 = new Thread(LockTest);
            var thread2 = new Thread(LockTest);
            Debug.WriteLine(DateTime.Now.Millisecond);
            thread1.Start(tp1);
            while (tp1.LockDate == null) { Thread.Sleep(1); }
            Debug.WriteLine(DateTime.Now.Millisecond);
            thread2.Start(tp2);
            while (tp2.LockDate == null) { Thread.Sleep(1); }
            Debug.WriteLine(DateTime.Now.Millisecond);
            Assert.IsTrue(tp1.LockDate.Value.AddMilliseconds(lockLength) > tp2.LockDate);
        }

        private void LockTest(object arg)
        {
            var tp = arg as ThreadParam;
            var context = new StormTestContext();
            using (context.Database.BeginTransaction())
            {
                LockTax(context, policy.Taxes[tp.TaxIndex].TaxId);
                tp.LockDate = DateTime.Now;
                Thread.Sleep(lockLength);
            }
        }

        private void LockTax(StormTestContext context, int taxId)
        {
            context.Database
                .ExecuteSqlCommand("SELECT * FROM model.tax WITH (updlock) WHERE tax_id = " + taxId);
        }
    }
}
