namespace StormCITest.Tests
{
    using System;
    using System.Linq;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;
    using StormTestProject.StormSchema.SomeSchema;

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
        public static EntityWithGuid EntityWithGuid(Guid guid)
        {
            return new EntityWithGuid
            {
                Id = guid,
                ADate = new DateTime(2017, 1, 1),
                ADatetime = new DateTime(2017, 1, 2, 1, 2, 3),
                ADatetime2 = new DateTime(2017, 1, 3, 1, 2, 3),
                AFloat = 223.456,
                AOffset = new DateTimeOffset(2017, 1, 4, 1, 2, 3, TimeSpan.FromMinutes(10)),
                AReal = (float)234.567,
                ASmalldatetime = new DateTime(2017, 1, 5, 1, 2, 0), //precision is one minute
                ATime = TimeSpan.FromMinutes(30),
            };
        }

        public static entity_with_sequence EfEntityWithSequence()
        {
            return new entity_with_sequence()
            {
                a_char = "a", // length = 1
                a_varchar = "1231231233453ffqef4vwt4v4v4tvw4vwrfvbwb",
                a_text = "r134fg245g254v45v245vfvv54versfvw43g5v4trtv34",
                a_nchar = "0987654321",
                a_nvarchar = "фыаыфв89-01346759-13y64r703478fh081374gh07bh31408cbh8134bhch",
                a_ntext = "фыавфыва39qruvhn94urbnv43rbnvu94nr9vn94rvnu94nvu9n49vn9p4n9p4rnv9pu",
                a_xml = "<xml />", // simplest xml that reads the same way as it writes
                a_binary = Enumerable.Range(1, 1000).Select(x => (byte)x).ToArray(),
                a_varbinary = Enumerable.Range(100, 5000).Select(x => (byte)x).ToArray(),
                a_image = null
            };
        }

        public static SmallentityWithSequence SmallEntityWithSequence(int len)
        {
            return new SmallentityWithSequence
            {
                AChar = "a", // length = 1
                AVarchar = "1231231233453ffqef4vwt4v4v4tvw4vwrfvbwb",
                AText = "r134fg245g254v45v245vfvv54versfvw43g5v4trtv34",
            };
        }

        public static SmallentityWithSequence SmallEntityWithSequenceForUpdate(int id)
        {
            return new SmallentityWithSequence
            {
                Id=id,
                AChar = "b", // length = 1
                AVarchar = "12312312334",
                AText = "r134fg245g254v45v",
            };
        }

        public static EntityWithSequence EntityWithSequence(int len)
        {
            return new EntityWithSequence
            {
                AChar = "a", // length = 1
                AVarchar = "1231231233453ffqef4vwt4v4v4tvw4vwrfvbwb",
                AText = "r134fg245g254v45v245vfvv54versfvw43g5v4trtv34",
                ANchar = "0987654321",
                ANvarchar = "фыаыфв89-01346759-13y64r703478fh081374gh07bh31408cbh8134bhch",
                ANtext = "фыавфыва39qruvhn94urbnv43rbnvu94nr9vn94rvnu94nvu9n49vn9p4n9p4rnv9pu",
                AXml = "<xml />", // simplest xml that reads the same way as it writes
                ABinary = Enumerable.Range(1, 1000).Select(x => (byte)x).ToArray(),
                AVarbinary = Enumerable.Range(100, len).Select(x => (byte)x).ToArray(),
                AImage = null
            };
        }

        public static entity_with_multikey EfEntityWithMultikey(int i)
        {
            return new entity_with_multikey
            {
                id_1 = i,
                id_2 = "id2",
                content = "content " + i
            };
        }

        public static EntityWithMultikey EntityWithMultikey(int i)
        {
            return new EntityWithMultikey
            {
                Id1 = i,
                Id2 = "id2",
                Content = "content " + i
            };
        }
    }
}
