namespace Tests.Unit.Definitions
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    internal class ResultListsShouldBeEqual
    {
        private readonly Storage storage;

        public ResultListsShouldBeEqual(Storage storage)
        {
            this.storage = storage;
        }

        [Then(@"'(.*)' list should be equal to")]
        public void ThenListShouldBeEqualTo(string resultKey, Table expected)
        {
            CollectionAssert.AreEqual(expected.ToStringList(), storage.Get<List<string>>(resultKey));
        }
    }
}
