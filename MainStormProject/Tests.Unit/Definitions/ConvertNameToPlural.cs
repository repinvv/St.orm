namespace Tests.Unit.Definitions
{
    using StormGenerator.Common;
    using TechTalk.SpecFlow;

    [Binding]
    public class ConvertNameToPlural
    {
        private readonly SimpleTestingContext context;

        public ConvertNameToPlural(SimpleTestingContext context)
        {
            this.context = context;
        }

        [When(@"I convert '(.*)' to plural '(.*)'")]
        public void WhenIConvertToPlural(string sourceKey, string resultKey)
        {
            var source = context.Get<string>(sourceKey);
            var result = new NameCreator().CreatePluralName(source);
            context.Set(resultKey, result);
        }
    }
}
