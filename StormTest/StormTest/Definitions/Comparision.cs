using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StormTest.Entities;
using StormTest.Services;
using TechTalk.SpecFlow;
using TestingContextCore.PublicMembers;

namespace StormTest.Definitions
{
    [Binding]
    public class Comparision
    {
        private readonly Storage storage;
        private readonly ComparisionService comparisionService;

        public Comparision(Storage storage, ComparisionService comparisionService)
        {
            this.storage = storage;
            this.comparisionService = comparisionService;
        }

        [Then(@"Company '(.*)' should be equal to first of source companies")]
        public void ThenCompanyShouldBeTheSameAsFirstOfSourceCompanies(string key)
        {
            var companies = storage.Get<List<Company>>();
            var company = storage.Get<Company>(key);
            Assert.IsTrue(comparisionService.CompaniesAreEqual(company, companies.First()));
        }

        [Then(@"Company '(.*)' should be equal to company '(.*)'")]
        public void ThenCompanyShouldBeEqualToCompany(string key1, string key2)
        {
            var company1 = storage.Get<Company>(key1);
            var company2 = storage.Get<Company>(key2);
            Assert.IsTrue(comparisionService.CompaniesAreEqual(company1, company2));
        }
    }
}
