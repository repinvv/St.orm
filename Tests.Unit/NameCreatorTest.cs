namespace Tests.Unit
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormGenerator.ModelsCollection;

    [TestClass]
    public class NameCreatorTest
    {
        private Dictionary<string, string> Samples
            = new Dictionary<string, string>
              {
                  { "somename", "Somename" },
                  { "some_name", "SomeName" },
                  { "SOME_NAME", "SomeName" },
                  { "SOMENAME", "Somename" },
                  { "SomeName", "SomeName" },
                  { "SomeName_OfSomeone", "SomeNameOfSomeone" },
                  { "someName", "SomeName"}
              };
            
        [TestMethod]
        public void CreateCamelCaseName_SucceedsOnSamples()
        {
            // setup
            var target = new NameCreator();

            foreach (var sample in Samples)
            {
                // act
                var result = target.CreateCamelCaseName(sample.Key);

                // verify
                Assert.AreEqual(sample.Value, result);
            }
        }
    }
}
