namespace Tests.Unit.Definitions
{
    using System.Collections.Generic;
    using StormGenerator.Common;
    using TechTalk.SpecFlow;

    [Binding]
    public class NormalizeNameList
    {
        private readonly SimpleTestingContext context;

        public NormalizeNameList(SimpleTestingContext context)
        {
            this.context = context;
        }

        [When(@"I normalize '(.*)' list to '(.*)' list")]
        public void WhenINormalizeListToList(string sourceKey, string resultKey)
        {
            var source = context.Get<List<string>>(sourceKey);
            var result = new NameNormalizer().NormalizeNames(source);
            context.Set(resultKey, result);
        }
    }
}
