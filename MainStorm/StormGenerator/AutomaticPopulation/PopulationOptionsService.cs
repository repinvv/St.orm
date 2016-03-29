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
        }

        public AutomaticPopulationOptions AutomaticPopulationOptions { get; }
    }
}
