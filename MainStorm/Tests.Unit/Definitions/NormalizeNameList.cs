namespace Tests.Unit.Definitions
{
    using System.Collections.Generic;
    using StormGenerator.Common;
    using TechTalk.SpecFlow;
    using TestingContextCore.PublicMembers;

    [Binding]
    public class NormalizeNameList
    {
        private readonly Storage storage;

        public NormalizeNameList(Storage storage)
        {
            this.storage = storage;
        }

        [When(@"I normalize '(.*)' list to '(.*)' list")]
        public void WhenINormalizeListToList(string sourceKey, string resultKey)
        {
            var source = storage.Get<List<string>>(sourceKey);
            var result = new NameNormalizer().NormalizeNames(source);
            storage.Set(result, resultKey);
        }


    }
}
