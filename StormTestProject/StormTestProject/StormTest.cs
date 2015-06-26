using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StormTestProject
{
    using System.Data.Entity;
    using System.Diagnostics;
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

                var watch = Stopwatch.StartNew();
                var query1 = from p in context.Policies select new { p, t = p.Taxes, c = p.Comments, a = p.Assignments };
                
                var result1 = query1.ToList();
                Debug.WriteLine("joined count " + result1.Count);
                watch.Stop();
                Debug.WriteLine("joined " + watch.Elapsed);

                watch = Stopwatch.StartNew();
                var policies = context.Policies.AsNoTracking().ToList();
                var assignments = context.Policies.SelectMany(x => x.Assignments).AsNoTracking().ToList();
                var taxes = context.Policies.SelectMany(x => x.Taxes).AsNoTracking().ToList();
                var comments = context.Policies.SelectMany(x => x.Comments).AsNoTracking().ToList();
                watch.Stop();
                Debug.WriteLine("p count " + policies.Count);
                Debug.WriteLine("a count " + assignments.Count);
                Debug.WriteLine("t count " + taxes.Count);
                Debug.WriteLine("c count " + comments.Count);
                Debug.WriteLine("regular " + watch.Elapsed);

                watch = Stopwatch.StartNew();
                var entities = context.Policies.Include(x => x.Assignments).Include(x => x.Taxes).Include(x => x.Comments).ToList();
                watch.Stop();
                Debug.WriteLine("include " + watch.Elapsed);

                watch = Stopwatch.StartNew();
                query1 = from p in context.Policies select new { p, t = p.Taxes, c = p.Comments, a = p.Assignments };

                result1 = query1.ToList();
                Debug.WriteLine("joined count " + result1.Count);
                watch.Stop();
                Debug.WriteLine("joined " + watch.Elapsed);

                watch = Stopwatch.StartNew();
                policies = context.Policies.AsNoTracking().ToList();
                assignments = context.Policies.SelectMany(x => x.Assignments).AsNoTracking().ToList();
                taxes = context.Policies.SelectMany(x => x.Taxes).AsNoTracking().ToList();
                comments = context.Policies.SelectMany(x => x.Comments).AsNoTracking().ToList();
                watch.Stop();
                Debug.WriteLine("p count " + policies.Count);
                Debug.WriteLine("a count " + assignments.Count);
                Debug.WriteLine("t count " + taxes.Count);
                Debug.WriteLine("c count " + comments.Count);
                Debug.WriteLine("regular " + watch.Elapsed);

                watch = Stopwatch.StartNew();
                entities = context.Policies.Include(x => x.Assignments).Include(x => x.Taxes).Include(x => x.Comments).ToList();
                watch.Stop();
                Debug.WriteLine("include " + watch.Elapsed);
            }
        }
    }
}
