using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StormTestProject.StormModel;

namespace StormCITest.Tests
{
    [TestClass]
    public class ComparisionTest
    {
        private const int iterations = 10000000;

        private bool Equality1(EntityWithGuid left, EntityWithGuid right)
        {
            if (left.ADate != right.ADate) return false;
            if (left.ADatetime != right.ADatetime) return false;
            if (left.ADatetime2 != right.ADatetime2) return false;
            if (left.AOffset != right.AOffset) return false;
            if (left.ASmalldatetime != right.ASmalldatetime) return false;
            if (left.ATime != right.ATime) return false;
            if (left.Id != right.Id) return false;
            if (left.AFloat != right.AFloat) return false;
            if (left.AReal != right.AReal) return false;
            return true;
        }

        private bool Equality2(EntityWithGuid left, EntityWithGuid right)
        {
            return left.ADate == right.ADate
                   && left.ADatetime == right.ADatetime
                   && left.ADatetime2 == right.ADatetime2
                   && left.AOffset == right.AOffset
                   && left.ASmalldatetime == right.ASmalldatetime
                   && left.ATime == right.ATime
                   && left.Id == right.Id
                   && left.AFloat == right.AFloat
                   && left.AReal == right.AReal;
        }

        private long measure1(EntityWithGuid left, EntityWithGuid right)
        {
            var watch = Stopwatch.StartNew();
            bool eq;
            for (int i = 0; i < iterations; i++)
            {
                eq = Equality1(left, right);
            }

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private long measure2(EntityWithGuid left, EntityWithGuid right)
        {
            var watch = Stopwatch.StartNew();
            bool eq;
            for (int i = 0; i < iterations; i++)
            {
                eq = Equality2(left, right);
            }

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        [TestMethod, Ignore]
        public void EqualityTest()
        {
            var e1 = new EntityWithGuid
            {
                ADate = new DateTime(1900, 10, 1),
                ADatetime = new DateTime(1900, 10, 1),
                ADatetime2 = new DateTime(1900, 10, 1),
                AOffset = new DateTime(1900, 10, 1),
                ASmalldatetime = new DateTime(1900, 10, 1),
                ATime = new DateTime(1900, 10, 1),
                Id = Guid.NewGuid(),
                AFloat = 100,
                AReal = 200
            };
            var e2 = new EntityWithGuid
            {
                ADate = new DateTime(1900, 10, 1),
                ADatetime = new DateTime(1900, 10, 1),
                ADatetime2 = new DateTime(1900, 10, 1),
                AOffset = new DateTime(1900, 10, 1),
                ASmalldatetime = new DateTime(1900, 10, 1),
                ATime = new DateTime(1900, 10, 2),
                Id = Guid.NewGuid(),
                AFloat = 200,
                AReal = 300
            };
            var e3 = new EntityWithGuid
            {
                ADate = new DateTime(1900, 10, 1),
                ADatetime = new DateTime(1900, 10, 2),
                ADatetime2 = new DateTime(1900, 10, 2),
                AOffset = new DateTime(1900, 10, 2),
                ASmalldatetime = new DateTime(1900, 10, 2),
                ATime = new DateTime(1900, 10, 2),
                Id = Guid.NewGuid(),
                AFloat = 200,
                AReal = 300
            };
            var eq1 = measure1(e1, e1);
            var eq2 = measure2(e1, e1);

            eq1 = measure1(e1, e1);
            eq2 = measure2(e1, e1);
            Console.WriteLine(eq1 + " " + eq2);

            eq1 = measure1(e1, e2);
            eq2 = measure2(e1, e2);
            Console.WriteLine(eq1 + " " + eq2);

            eq1 = measure1(e1, e3);
            eq2 = measure2(e1, e3);
            Console.WriteLine(eq1 + " " + eq2);
        }

        [TestMethod]
        public void ManualComparisionTest()
        {
            var a = new EntityWithId();
            var b = new EntityWithId();
            var c = a.Equals(b);
        }
    }
}
