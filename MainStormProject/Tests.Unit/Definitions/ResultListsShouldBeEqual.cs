namespace Tests.Unit.Definitions
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;

    internal class ResultListsShouldBeEqual
    {
        private readonly SimpleTestingContext context;

        public ResultListsShouldBeEqual(SimpleTestingContext context)
        {
            this.context = context;
        }

        [Then(@"'(.*)' list should be equal to")]
        public void ThenListShouldBeEqualTo(string resultKey, Table expected)
        {
            CollectionAssert.AreEqual(expected.ToStringList(), context.Get<List<string>>(resultKey));
        }
    }
}
