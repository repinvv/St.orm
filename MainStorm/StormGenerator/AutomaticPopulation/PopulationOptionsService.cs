namespace StormGenerator.AutomaticPopulation
{
    using StormGenerator.Settings;

    internal class PopulationOptionsService
    {
        public PopulationOptionsService(Options options)
        {
            AutomaticPopulationOptions
                = options.AutomaticPopulationOptions ??
                  new AutomaticPopulationOptions
                  {
                      CamelCaseNames = true,
                      PluralNames = true
                  };
            if (AutomaticPopulationOptions.MaxChildModels < 1)
            {
                AutomaticPopulationOptions.MaxChildModels = 3;
            }
        }

        public AutomaticPopulationOptions AutomaticPopulationOptions { get; }
    }
}
