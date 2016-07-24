namespace StormCITest.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;
    using StormTestProject.StormSchema.SomeSchema;

    public class Compare
    {
        public static void EntityWithSequence(entity_with_sequence efEntity, EntityWithSequence entity)
        {
            Assert.AreEqual(efEntity.id, entity.Id);
            Assert.AreEqual(efEntity.a_char, entity.AChar);
            Assert.AreEqual(efEntity.a_nchar, entity.ANchar);
            Assert.AreEqual(efEntity.a_varchar, entity.AVarchar);
            Assert.AreEqual(efEntity.a_nvarchar, entity.ANvarchar);
            Assert.AreEqual(efEntity.a_ntext, entity.ANtext);
            Assert.AreEqual(efEntity.a_xml, entity.AXml);
            CollectionAssert.AreEqual(efEntity.a_binary, entity.ABinary);
            CollectionAssert.AreEqual(efEntity.a_varbinary, entity.AVarbinary);
            Assert.IsNull(entity.AImage);
        }

        public static void EntityWithSequence(EntityWithSequence src, EntityWithSequence entity)
        {
            Assert.AreEqual(src.Id, entity.Id);
            Assert.AreEqual(src.AChar, entity.AChar);
            Assert.AreEqual(src.ANchar, entity.ANchar);
            Assert.AreEqual(src.AVarchar, entity.AVarchar);
            Assert.AreEqual(src.ANvarchar, entity.ANvarchar);
            Assert.AreEqual(src.ANtext, entity.ANtext);
            Assert.AreEqual(src.AXml, entity.AXml);
            CollectionAssert.AreEqual(src.ABinary, entity.ABinary);
            CollectionAssert.AreEqual(src.AVarbinary, entity.AVarbinary);
            Assert.IsNull(entity.AImage);
        }

        public static void EntityWithId(entity_with_id efEntity, EntityWithId entity)
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

        public static void EntityWithId(EntityWithId src, EntityWithId entity)
        {
            Assert.AreEqual(src.Id, entity.Id);
            Assert.AreEqual(src.AInt, entity.AInt);
            Assert.AreEqual(src.ANumeric, entity.ANumeric);
            Assert.AreEqual(src.ABit, entity.ABit);
            Assert.AreEqual(src.ASmallint, entity.ASmallint);
            Assert.AreEqual(src.ADecimal, entity.ADecimal);
            Assert.AreEqual(src.ASmallmoney, entity.ASmallmoney);
            Assert.AreEqual(src.ATinyint, entity.ATinyint);
            Assert.AreEqual(src.AMoney, entity.AMoney);
        }

        public static void EntityWithGuid(entity_with_guid efEntity, EntityWithGuid entity)
        {
            Assert.AreEqual(efEntity.id, entity.Id);
            Assert.AreEqual(efEntity.a_real, entity.AReal);
            Assert.AreEqual(efEntity.a_date, entity.ADate);
            Assert.AreEqual(efEntity.a_datetime, entity.ADatetime);
            Assert.AreEqual(efEntity.a_datetime2, entity.ADatetime2);
            Assert.AreEqual(efEntity.a_float, entity.AFloat);
            Assert.AreEqual(efEntity.a_offset, entity.AOffset);
            Assert.AreEqual(efEntity.a_smalldatetime, entity.ASmalldatetime);
            Assert.AreEqual(efEntity.a_time, entity.ATime);
        }

        public static void EntityWithGuid(EntityWithGuid src, EntityWithGuid entity)
        {
            Assert.AreEqual(src.Id, entity.Id);
            Assert.AreEqual(src.AReal, entity.AReal);
            Assert.AreEqual(src.ADate, entity.ADate);
            Assert.AreEqual(src.ADatetime, entity.ADatetime);
            Assert.AreEqual(src.ADatetime2, entity.ADatetime2);
            Assert.AreEqual(src.AFloat, entity.AFloat);
            Assert.AreEqual(src.AOffset, entity.AOffset);
            Assert.AreEqual(src.ASmalldatetime, entity.ASmalldatetime);
            Assert.AreEqual(src.ATime, entity.ATime);
        }

        public static void EntityWithMultikey(entity_with_multikey efEntity, EntityWithMultikey entity)
        {
            Assert.AreEqual(efEntity.id_1, entity.Id1);
            Assert.AreEqual(efEntity.id_2, entity.Id2);
            Assert.AreEqual(efEntity.content, entity.Content);
        }
    }
}
