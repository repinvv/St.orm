namespace StormTestProject.Tests.Basic
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.Tests.Infrastructure;

    [TestClass]
    public class StormTestInsert
    {
        [TestMethod]
        public void Storm_InsertsAndReadsModels()
        {
           TestConfig.Get<StormInsertAndReadTest>().Storm_InsertsAndReadsModels();
        }
    }
}
