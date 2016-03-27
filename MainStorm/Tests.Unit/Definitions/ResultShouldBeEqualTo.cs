namespace Tests.Unit.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class ResultShouldBeEqualTo
    {
        private readonly Storage storage;

        public ResultShouldBeEqualTo(Storage storage)
        {
            this.storage = storage;
        }

        [Then(@"'(.*)' should be equal to '(.*)'")]
        public void ThenShouldBeEqualTo(string resultKey, string result)
        {
            Assert.AreEqual(result, storage.Get<string>(resultKey));
        }
    }
}
