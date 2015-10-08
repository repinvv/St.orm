namespace Tests.Unit.Definitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;

    [Binding]
    public class ResultShouldBeEqualTo
    {
        private readonly SimpleTestingContext context;

        public ResultShouldBeEqualTo(SimpleTestingContext context)
        {
            this.context = context;
        }

        [Then(@"'(.*)' should be equal to '(.*)'")]
        public void ThenShouldBeEqualTo(string resultKey, string result)
        {
            Assert.AreEqual(result, context.Get<string>(resultKey));
        }
    }
}
