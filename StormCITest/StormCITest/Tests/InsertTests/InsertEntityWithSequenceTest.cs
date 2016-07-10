namespace StormCITest.Tests.InsertTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormCITest.EFSchema;
    using StormTestProject.StormSchema;
    using StormTestProject.StormSchema.SomeSchema;

    [TestClass]
    public class InsertEntityWithSequenceTest : ContextInTransaction
    {
        [TestMethod]
        public void Insert_EnitityWithSequence_Single()
        {
            // arrange
            var entity = Create.EntityWithSequence(1000);

            // act
            MsSqlCi.Insert(entity, conn);

            // assert
            var efEntity = context.entity_with_sequence.First(x => x.id == entity.Id);
            Compare.EntityWithSequence(efEntity, entity);
        }

        
    }
}
