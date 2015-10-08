namespace BasicFeaturesTest.Tests.Basic
{
    using BasicFeaturesTest.Tests.Basic.ActualTests;
    using BasicFeaturesTest.Tests.Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
