namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.DbModels;

    internal class AutomaticModelConfigsPopulation
    {
        private readonly ModelConfigPopulation configPopulation;

        public AutomaticModelConfigsPopulation(ModelConfigPopulation configPopulation)
        {
            this.configPopulation = configPopulation;
        }

        public List<ModelConfig> PopulateConfigs(List<ModelConfig> existingConfigs, List<Table> tables)
        {
            var existingConfigsDict = existingConfigs.ToDictionary(x => x.DbTableId);
            var unconfiguredTables = tables.Where(x => !existingConfigsDict.ContainsKey(x.Id));
            return existingConfigs.Concat(unconfiguredTables
                .Select(x => configPopulation.PopulateConfig(x, tables)))
                .ToList();
        }
    }
}
