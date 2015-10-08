namespace Tests.Unit.Definitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class HaveAListDefinedAs
    {
        private readonly SimpleTestingContext context;

        public HaveAListDefinedAs(SimpleTestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have a '(.*)' list defined as")]
        public void GivenIHaveAListDefinedAs(string key, Table table)
        {
            context.Set(key, table.ToStringList());
        }
    }
}
