namespace StormCITest.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    public class Compares
    {
        public static void CompareEntity(entity_with_id efEntity, EntityWithId entity)
        {
            Assert.AreEqual(efEntity.id, entity.Id);
            Assert.AreEqual(efEntity.a_int, entity.AInt);
            Assert.AreEqual(efEntity.a_numeric, entity.ANumeric);
            Assert.AreEqual(efEntity.a_bit, entity.ABit);
            Assert.AreEqual(efEntity.a_smallint, entity.ASmallint);
            Assert.AreEqual(efEntity.a_decimal, entity.ADecimal);
            Assert.AreEqual(efEntity.a_smallmoney, entity.ASmallmoney);
            Assert.AreEqual(efEntity.a_tinyint, entity.ATinyint);
            Assert.AreEqual(efEntity.a_money, entity.AMoney);
        }

        public static void CompareEntity(entity_with_guid efEntity, EntityWithGuid entity)
        {
            Assert.AreEqual(efEntity.id, entity.Id);
            Assert.IsTrue(efEntity.a_real == entity.AReal);
            Assert.AreEqual(efEntity.a_date, entity.ADate);
            Assert.AreEqual(efEntity.a_datetime, entity.ADatetime);
            Assert.AreEqual(efEntity.a_datetime2, entity.ADatetime2);
            Assert.AreEqual(efEntity.a_float, entity.AFloat);
            Assert.AreEqual(efEntity.a_offset, entity.AOffset);
            Assert.AreEqual(efEntity.a_smalldatetime, entity.ASmalldatetime);
            Assert.AreEqual(efEntity.a_time, entity.ATime);
        }
    }
}
