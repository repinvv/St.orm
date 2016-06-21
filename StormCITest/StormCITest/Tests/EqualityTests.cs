using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StormCITest.Tests
{
    using StormCITest.EFSchema;
    using StormTestProject.StormModel;

    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void OneOfTwo_NotEqual()
        {
            var e1 = new EntityWithMultikey { Id1 = 10, Id2 = "key1", };
            var e2 = new EntityWithMultikey { Id1 = 10, Id2 = "key2" };
            Assert.IsFalse(e1 == e2);
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void TwoOfTwoDifferentContent_Equal()
        {
            var e1 = new EntityWithMultikey { Id1 = 10, Id2 = "key1", Content = "content1" };
            var e2 = new EntityWithMultikey { Id1 = 10, Id2 = "key2", Content = "content2" };
            Assert.IsFalse(e1 == e2);
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void SameWithNoKeys_Equal()
        {
            var e1 = new EntityWithMultikey();
            var e2 = e1;
            Assert.IsTrue(e1 == e2);
            Assert.AreEqual(e1, e2);
        }

        [TestMethod]
        public void WithDefaultKeys_NotEqual()
        {
            var e1 = new EntityWithMultikey { Id2 = "key1" };
            var e2 = new EntityWithMultikey { Id2 = "key1" };
            Assert.IsFalse(e1 == e2);
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void KeylessWithSameContent_Equal()
        {
            var e1 = new EntityWithoutKey { Content = "content", Value = 10 };
            var e2 = new EntityWithoutKey { Content = "content", Value = 10 };
            Assert.IsTrue(e1 == e2);
            Assert.AreEqual(e1, e2);
        }


        [TestMethod]
        public void KeylessWithDifferentContent_NotEqual()
        {
            var e1 = new EntityWithoutKey { Content = "content", Value = 10 };
            var e2 = new EntityWithoutKey { Content = "content2", Value = 13 };
            Assert.IsFalse(e1 == e2);
            Assert.AreNotEqual(e1, e2);
        }
    }
}
