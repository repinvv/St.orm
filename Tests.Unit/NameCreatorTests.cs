namespace Tests.Unit
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormGenerator.Common;

    [TestClass]
    public class NameCreatorTests
    {
        private readonly Dictionary<string, string> camelCaseSamples
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

        private readonly Dictionary<string, string> pluralSamples
            = new Dictionary<string, string>
              {
                  { "ballista", "ballistae" },
                  { "class", "classes"},
                  { "box", "boxes" },
                  { "byte", "bytes" },
                  { "bolt", "bolts" },
                  { "fish", "fishes" },
                  { "guy", "guys" },
                  { "ply", "plies" }
              };
            
        [TestMethod]
        public void CreateCamelCaseName_SucceedsOnSamples()
        {
            // setup
            var target = new NameCreator();

            foreach (var sample in camelCaseSamples)
            {
                // act
                var result = target.CreateCamelCaseName(sample.Key);

                // verify
                Assert.AreEqual(sample.Value, result);
            }
        }

        [TestMethod]
        public void CreatePluralName_SucceedsOnSamples()
        {
            // setup
            var target = new NameCreator();

            foreach (var sample in pluralSamples)
            {
                // act
                var result = target.CreatePluralName(sample.Key);

                // verify
                Assert.AreEqual(sample.Value, result);
            }
        }
    }
}
