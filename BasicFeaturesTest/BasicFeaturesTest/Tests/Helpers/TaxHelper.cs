namespace StormTestProject.Tests.Helpers
{
    using System;
    using StormTestProject.StormModel;

    internal class TaxHelper
    {
        private int i = 0;

        public Tax CreateTax()
        {
            return new Tax { Created = DateTime.Now, Updated = DateTime.Now, Amount = (decimal)(++i + 0.1) };
        }
    }
}
