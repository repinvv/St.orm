namespace Tests.Unit.Definitions
{
    using StormGenerator.Common;
    using TechTalk.SpecFlow;

    [Binding]
    public class ConvertNameToCamelCase
    {
        private readonly SimpleTestingContext context;

        public ConvertNameToCamelCase(SimpleTestingContext context)
        {
            this.context = context;
        }

        [When(@"I convert '(.*)' to CamelCase '(.*)'")]
        public void WhenIConvertToCamelCase(string sourceKey, string resultKey)
        {
            var source = context.Get<string>(sourceKey);
            var result = new NameCreator().CreateCamelCaseName(source);
            context.Set(resultKey, result);
        }
    }
}
