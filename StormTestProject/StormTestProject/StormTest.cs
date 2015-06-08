using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StormTestProject
{
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
                var currency = new Currency { Name = "Dollar", CurrencyCode = "usd" };
                var department = new Department { Name = "dept1" };
                context.Countries.Add(country);
                context.Currencies.Add(currency);
                // context.Departments.Add(department);
                context.SaveChanges();


                var policy = new Policy
                {
                    Name = "my policy",
                    CountryId = country.CountryId,
                    CurrencyId = currency.CurrencyId,
                    Coverages = new List<Coverage>
                    {
                        new Coverage
                        {
                            Departments = new List<Department> {department}
                        }
                    }
                };

                context.Policies.Add(policy);
                context.SaveChanges();

                Assert.AreEqual(trans, context.Database.CurrentTransaction);
            }
        }
    }
}
