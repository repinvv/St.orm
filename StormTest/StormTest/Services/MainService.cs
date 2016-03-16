using System.Collections.Generic;
using System.Linq;
using StormTest.DAL;
using StormTest.Entities;

namespace StormTest.Services
{
    public class MainService
    {
        private readonly ConversionService conversionService;
        private readonly MainDal mainDal;

        public MainService(ConversionService conversionService, MainDal mainDal)
        {
            this.conversionService = conversionService;
            this.mainDal = mainDal;
        }

        public void SaveCompanies(List<Company> companies)
        {
            var dalCompanies = companies.Select(conversionService.CompanyToDal).ToList();
            mainDal.InsertCompanies(dalCompanies);
        }

        public Company LoadCompany(string name)
        {
            var company = mainDal.LoadCompany(name);
            return conversionService.DalToCompany(company);
        }

        public void UpdateCompany(Company company)
        {
            // todo2
            var dal = conversionService.CompanyToDal(company);
            mainDal.UpdateCompany(dal);
        }
    }
}
