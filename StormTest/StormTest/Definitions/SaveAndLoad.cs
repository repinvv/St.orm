using System.Collections.Generic;
using StormTest.Entities;
using StormTest.Services;
using TechTalk.SpecFlow;
using TestingContextCore.PublicMembers;

namespace StormTest.Definitions
{
    [Binding]
    public class SaveAndLoad
    {
        private readonly Storage storage;
        private readonly MainService mainService;

        public SaveAndLoad(Storage storage, MainService mainService)
        {
            this.storage = storage;
            this.mainService = mainService;
        }

        [When(@"I save companies")]
        public void WhenISaveCompanies()
        {
            var companies = storage.Get<List<Company>>();
            mainService.SaveCompanies(companies);
        }

        [When(@"I load company '(.*)' as '(.*)'")]
        public void WhenILoadCompany(string name, string key)
        {
            var company = mainService.LoadCompany(name);
            storage.Set(company, key);
        }

        [When(@"I update company '(.*)' in the database")]
        public void WhenIUpdateCompanyInTheDatabase(string key)
        {
            var company = storage.Get<Company>(key);
            mainService.UpdateCompany(company);
        }
    }
}
