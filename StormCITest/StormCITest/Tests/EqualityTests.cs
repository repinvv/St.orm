﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StormCITest.Tests
{
    using StormTestProject.StormSchema;

    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void Equality_OneOfTwo_NotEqual()
        {
            var e1 = new EntityWithMultikey { Id1 = 10, Id2 = "key1", };
            var e2 = new EntityWithMultikey { Id1 = 10, Id2 = "key2" };
            Assert.IsFalse(e1 == e2);
            Assert.IsFalse(e1.Equals(e2));
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void Equality_TwoOfTwoDifferentContent_Equal()
        {
            var e1 = new EntityWithMultikey { Id1 = 10, Id2 = "key1", Content = "content1" };
            var e2 = new EntityWithMultikey { Id1 = 10, Id2 = "key2", Content = "content2" };
            Assert.IsFalse(e1 == e2);
            Assert.IsFalse(e1.Equals(e2));
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void Equality_SameWithNoKeys_Equal()
        {
            var e1 = new EntityWithMultikey();
            var e2 = e1;
            Assert.IsTrue(e1 == e2);
            Assert.IsTrue(e1.Equals(e2));
            Assert.AreEqual(e1, e2);
        }

        [TestMethod]
        public void Equality_WithDefaultKeys_NotEqual()
        {
            var e1 = new EntityWithMultikey { Id2 = "key1" };
            var e2 = new EntityWithMultikey { Id2 = "key1" };
            Assert.IsFalse(e1 == e2);
            Assert.IsFalse(e1.Equals(e2));
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void Equality_KeylessWithSameContent_Equal()
        {
            var e1 = new EntityWithoutKey { Content = "content", Value = 10 };
            var e2 = new EntityWithoutKey { Content = "content", Value = 10 };
            Assert.IsTrue(e1 == e2);
            Assert.IsTrue(e1.Equals(e2));
            Assert.AreEqual(e1, e2);
        }


        [TestMethod]
        public void Equality_KeylessWithDifferentContent_NotEqual()
        {
            var e1 = new EntityWithoutKey { Content = "content", Value = 10 };
            var e2 = new EntityWithoutKey { Content = "content2", Value = 13 };
            Assert.IsFalse(e1 == e2);
            Assert.IsFalse(e1.Equals(e2));
            Assert.AreNotEqual(e1, e2);
        }

        [TestMethod]
        public void Equality_Nulls_Equal()
        {
            EntityWithoutKey e1 = null;
            EntityWithoutKey e2 = null;
            Assert.IsTrue(e1 == e2);
            Assert.AreEqual(e1, e2);
        }

        [TestMethod]
        public void Equality_OneIsNull_NotEqual()
        {
            var e1 = new EntityWithoutKey();
            EntityWithoutKey e2 = null;
            Assert.IsFalse(e1 == e2);
            Assert.IsFalse(e1.Equals(e2));
            Assert.AreNotEqual(e1, e2);
            Assert.IsFalse(e2 == e1);
            Assert.AreNotEqual(e2, e1);
        }
    }
}
