namespace StormTestProject.Tests.Basic
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.Tests.Basic.ActualTests;
    using StormTestProject.Tests.Infrastructure;

    [TestClass]
    public class InsertTest
    {
        [TestMethod]
        public void Storm_InsertsAndReadsModels()
        {
           TestConfig.Get<ActualInsertTest>().Storm_InsertsAndReadsModels();
        }
    }
}
