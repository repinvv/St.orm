namespace Tests.Unit.Definitions
{
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class HaveAListDefinedAs
    {
        private readonly Storage storage;

        public HaveAListDefinedAs(Storage storage)
        {
            this.storage = storage;
        }

        [Given(@"I have a '(.*)' list defined as")]
        public void GivenIHaveAListDefinedAs(string key, Table table)
        {
            storage.Set(table.ToStringList(), key);
        }
    }
}
