namespace StormGenerator.AutomaticPopulation
{
    using StormGenerator.Common;
    using StormGenerator.Settings;

    internal class NamePopulation
    {
        private readonly NameCreator nameCreator;
        private readonly AutomaticPopulationOptions options;

        public NamePopulation(NameCreator nameCreator, PopulationOptionsService optionsService)
        {
            this.nameCreator = nameCreator;
            options = optionsService.AutomaticPopulationOptions;
        }

        public string CreateItemName(string name)
        {
            return options.CamelCaseNames ? nameCreator.CreateCamelCaseName(name) : name;
        }

        public string CreateRelationName(string name, bool isMultiple)
        {
            var output = options.CamelCaseNames ? nameCreator.CreateCamelCaseName(name) : name;
            return isMultiple && options.PluralNames ? nameCreator.CreatePluralName(output) : name;
        }
    }
}
