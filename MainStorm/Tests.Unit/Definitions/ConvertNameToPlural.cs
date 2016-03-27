namespace Tests.Unit.Definitions
{
    using StormGenerator.Common;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class ConvertNameToPlural
    {
        private readonly Storage storage;

        public ConvertNameToPlural(Storage storage)
        {
            this.storage = storage;
        }

        [When(@"I convert '(.*)' to plural '(.*)'")]
        public void WhenIConvertToPlural(string sourceKey, string resultKey)
        {
            var source = storage.Get<string>(sourceKey);
            var result = new NameCreator().CreatePluralName(source);
            storage.Set(result, resultKey);
        }
    }
}
