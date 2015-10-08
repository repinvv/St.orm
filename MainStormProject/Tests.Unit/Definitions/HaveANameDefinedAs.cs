namespace Tests.Unit.Definitions
{
    using TechTalk.SpecFlow;

    [Binding]
    public class HaveANameDefinedAs
    {
        private readonly SimpleTestingContext context;

        public HaveANameDefinedAs(SimpleTestingContext context)
        {
            this.context = context;
        }

        [Given(@"I have a '(.*)' defined as '(.*)'")]
        public void GivenIHaveADefinedAs(string key, string value)
        {
            context.Set(key, value);
        }
    }
}
