namespace Tests.Unit.Definitions
{
    using StormGenerator.Common;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class ConvertNameToCamelCase
    {
        private readonly Storage storage;

        public ConvertNameToCamelCase(Storage storage)
        {
            this.storage = storage;
        }

        [When(@"I convert '(.*)' to CamelCase '(.*)'")]
        public void WhenIConvertToCamelCase(string sourceKey, string resultKey)
        {
            var source = storage.Get<string>(sourceKey);
            var result = new NameCreator().CreateCamelCaseName(source);
            storage.Set(result, resultKey);
        }
    }
}
