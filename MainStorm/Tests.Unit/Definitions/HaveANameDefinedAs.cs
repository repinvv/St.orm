namespace Tests.Unit.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class HaveANameDefinedAs
    {
        private readonly Storage storage;

        public HaveANameDefinedAs(Storage storage)
        {
            this.storage = storage;
        }

        [Given(@"I have a '(.*)' defined as '(.*)'")]
        public void GivenIHaveADefinedAs(string key, string value)
        {
            storage.Set(value, key);
        }
    }
}
