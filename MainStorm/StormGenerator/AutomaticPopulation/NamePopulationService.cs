namespace StormGenerator.AutomaticPopulation
{
    using StormGenerator.Common;
    using StormGenerator.Settings;

    internal class NamePopulationService
    {
        private readonly NameCreator nameCreator;
        private readonly AutomaticPopulationOptions options;

        public NamePopulationService(NameCreator nameCreator, PopulationOptionsService optionsService)
        {
            this.nameCreator = nameCreator;
            options = optionsService.AutomaticPopulationOptions;
        }

        public string CreateItemName(string name)
        {
            return options.CamelCaseNames ? nameCreator.CreateCamelCaseName(name) : name;
        }

        public string CreateNavPropName(string name, bool isMultiple)
        {
            var output = options.CamelCaseNames ? nameCreator.CreateCamelCaseName(name) : name;
            return options.PluralNames ? nameCreator.CreatePluralName(output) : name;
        }
    }
}
