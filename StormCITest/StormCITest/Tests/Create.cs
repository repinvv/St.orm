namespace StormCITest.Tests
{
    using System;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;

    internal static class Create
    {
        public static entity_with_id EfEntityWithId(int intVal)
        {
            return new entity_with_id
            {
                a_bigint = 10L * 1000 * 1000 * 1000,
                a_int = intVal,
                a_numeric = 3123.22M, //precision is 6.2
                a_bit = true,
                a_smallint = 123,
                a_decimal = 222.223M,
                a_smallmoney = 333.334M,
                a_tinyint = 22,
                a_money = 444.44M
            };
        }

        public static EntityWithId EntityWithId(int intVal)
        {
            return new EntityWithId
            {
                ABigint = 10L * 1000 * 1000 * 1000,
                AInt = intVal,
                ANumeric = 3123.22M, //precision is 6.2
                ABit = true,
                ASmallint = 123,
                ADecimal = 222.223M,
                ASmallmoney = 333.334M,
                ATinyint = 22,
                AMoney = 444.44M
            };
        }

        public static EntityWithGuid EntityWithGuid(int i)
        {
            return new EntityWithGuid
            {
                Id = Guid.NewGuid(),
                ADate = new DateTime(2016, 1, 1),
                ADatetime = new DateTime(2016, 1, 2, 1, 2, 3),
                ADatetime2 = new DateTime(2016, 1, 3, 1, 2, 3),
                AFloat = 123.456,
                AOffset = new DateTimeOffset(2016, 1, 4, 1, 2, 3, TimeSpan.FromMinutes(10)),
                AReal = (float)234.567,
                ASmalldatetime = new DateTime(2016, 1, 5, 1, 2, 0), //precision is one minute
                ATime = TimeSpan.FromMinutes(20),
            };
        }
    }
}
