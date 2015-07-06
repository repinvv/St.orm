namespace StormTestProject.Tests.Helpers
{
    using System;
    using StormTestProject.StormModel;

    internal class CurrencyHelper
    {
        private readonly StormTestContext context;

        public CurrencyHelper(StormTestContext context)
        {
            this.context = context;
        }

        public Currency CreateAndSaveCurrency()
        {
            var currency = CreateCurrency();
            context.Storm.Save(currency);
            return currency;
        }

        public Currency CreateCurrency()
        {
            return new Currency { Name = "Dollar", CurrencyCode = "usd", Created = DateTime.Now, Updated = DateTime.Now };
        }
    }
}
