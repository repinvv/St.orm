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
    }
}
