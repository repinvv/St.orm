using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StormTestProject
{
    using System.Linq;

    [TestClass]
    public class StormTest
    {
        [TestMethod]
        public void test()
        {
            var context = new StormTestContext();
            using (var trans = context.Database.BeginTransaction())
            {
                var country = new Country { Name = "Russia", CountryCode = "ru" };
                var country2 = new Country { Name = "England", CountryCode = "uk" };
                var currency = new Currency { Name = "Dollar", CurrencyCode = "usd" };
                var department = new Department { Name = "dept1" };
                context.Countries.Add(country);
                context.Countries.Add(country2);
                context.Currencies.Add(currency);
                // context.Departments.Add(department);
                context.SaveChanges();


                var policy = new Policy
                                 {
                                     Name = "my policy",
                                     CountryId = country.CountryId,
                                     CurrencyId = currency.CurrencyId,
                                     Taxes = new List<Tax> { new Tax { Amount = (decimal)10.2 } }
                                 };

                context.Policies.Add(policy);
                context.SaveChanges();
                var query = context.Set<Policy>();

                var result = context.Storm.Get(query).First();
                var resultCountry = result.Country;
                var taxes = result.Taxes;

                Assert.AreEqual(trans, context.Database.CurrentTransaction);
            }
        }
    }
}
