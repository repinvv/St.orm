using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormCITest.Tests.ReadTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;
    using StormTestProject.StormSchema.SomeSchema;

    [TestClass]
    public class ReadEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Read_EntityWithSequence()
        {
            // arrange
            var efEntity = CreateFullEntity();
            context.entity_with_sequence.Add(efEntity);
            context.SaveChanges();

            // act
            var sql = "select * from some_schema.entity_with_sequence where id = @id";
            var parm = new[] { new SqlParameter("id", SqlDbType.Int) { Value = efEntity.id } };
            var entity = MsSqlCi
                .Materialize<EntityWithSequence>(sql, parm, conn)
                .First();

            // assert
            CompareEntities(efEntity, entity);
        }

        private void CompareEntities(entity_with_sequence efEntity, EntityWithSequence entity)
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

        private entity_with_sequence CreateFullEntity()
        {
            return new entity_with_sequence()
                   {
                       id = 1,
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
    }
}
