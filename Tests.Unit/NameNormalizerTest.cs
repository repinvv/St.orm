namespace Tests.Unit
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormGenerator.Common;

    [TestClass]
    public class NameNormalizerTest
    {
        [TestMethod]
        public void NormalizeNames_DoesItsJob()
        {
            // setup
            var list = new List<string>
                       {
                           "someNaem123",
                           "matchNaem",
                           "someNaem234",
                           "matchNaem",
                           "matchNaem",
                           "someNaem345",
                           "matchNaem2",
                           "matchNaem"
                       };

            var expected = new List<string>
                       {
                           "someNaem123",
                           "matchNaem",
                           "someNaem234",
                           "matchNaem3",
                           "matchNaem4",
                           "someNaem345",
                           "matchNaem2",
                           "matchNaem5"
                       };

            var target = new NameNormalizer();

            // act
            var result = target.NormalizeNames(list);

            // verify
            Assert.AreNotSame(result, list);
            CollectionAssert.AreEqual(result, list);
        }
    }
}
