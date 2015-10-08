namespace BasicFeaturesTest.Tests.Helpers
{
    using System;
    using BasicFeaturesTest.StormModel;

    internal class CountryHelper
    {
        private readonly StormTestContext context;

        public CountryHelper(StormTestContext context)
        {
            this.context = context;
        }

        public Country CreateAndSaveCountry()
        {
            var country = CreateCountry();
            context.Storm.Save(country);
            return country;
        }

        public Country CreateCountry()
        {
            return new Country { Name = "Russia", CountryCode = "ru", Created = DateTime.Now, Updated = DateTime.Now };
        }
    }
}
