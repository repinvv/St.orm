namespace StormTestProject.Tests.Basic
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StormTestProject.StormModel;
    using StormTestProject.Tests.Infrastructure;

    [TestClass]
    public class StormTestEf
    {
        [TestMethod]
        public void Ef_InsertsAndReadsWithGeneratedModels()
        {
            var context = new StormTestContext();
            using (var trans = context.Database.BeginTransaction())
            {
                var country = new Country { Name = "Russia", CountryCode = "ru" };
                var currency = new Currency { Name = "Dollar", CurrencyCode = "usd" };
                context.Countries.Add(country);
                context.Currencies.Add(currency);
                context.SaveChanges();

                var policy = new Policy
                                 {
                                     Name = "my policy",
                                     CountryId = country.CountryId,
                                     CurrencyId = currency.CurrencyId,
                                     Assignments =
                                         new List<Assignment>
                                             {
                                                 new Assignment(),
                                                 new Assignment(),
                                                 new Assignment(),
                                                 new Assignment(),
                                                 new Assignment(),
                                             },
                                     Taxes =
                                         new List<Tax>
                                             {
                                                 new Tax { Amount = (decimal)10.1 },
                                                 new Tax { Amount = (decimal)10.2 },
                                                 new Tax { Amount = (decimal)10.3 },
                                                 new Tax { Amount = (decimal)10.4 },
                                                 new Tax { Amount = (decimal)10.5 }
                                             },
                                     Comments =
                                         new List<Comment>
                                             {
                                                 new Comment { CommentText = "comment1" },
                                                 new Comment { CommentText = "comment2" },
                                                 new Comment { CommentText = "comment3" },
                                                 new Comment { CommentText = "comment4" },
                                                 new Comment { CommentText = "comment5" }
                                             }
                                 };

                context.Policies.Add(policy);
                context.SaveChanges();

                var query1 = from p in context.Policies select new { p, t = p.Taxes, c = p.Comments, a = p.Assignments };
                
                var result1 = query1.ToList();
            }
        }
    }
}
