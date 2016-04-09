namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.AutomaticPopulation.NavPropsPopulation;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.DbModels;

    internal class AutomaticModelConfigsPopulation
    {
        private readonly ModelConfigPopulation configPopulation;
        private readonly ConfigListNameNormalizer normalizer;
        private readonly NavPropConfigsPopulation navPropConfigsPopulation;

        public AutomaticModelConfigsPopulation(ModelConfigPopulation configPopulation,
            ConfigListNameNormalizer normalizer,
            NavPropConfigsPopulation navPropConfigsPopulation)
        {
            this.configPopulation = configPopulation;
            this.normalizer = normalizer;
            this.navPropConfigsPopulation = navPropConfigsPopulation;
        }

        public List<ModelConfig> PopulateConfigs(List<ModelConfig> existingConfigs, List<Table> tables)
        {
            var existingConfigsDict = existingConfigs.ToDictionary(x => x.DbTableId);
            var unconfiguredTables = tables.Where(x => !existingConfigsDict.ContainsKey(x.Id));
            var newConfigs = unconfiguredTables
                .Select(x => configPopulation.PopulateConfig(x, tables)).ToList();
            var configs = existingConfigs.Concat(newConfigs).ToList();
            navPropConfigsPopulation.PopulateNavProps(newConfigs, configs, tables);
            NormalizeNames(configs);
            newConfigs.ForEach(x => x.Id = x.NamespaceSuffix + "." + x.Name);
            return configs;
        }

        private void NormalizeNames(List<ModelConfig> configs)
        {
            var groups = configs.GroupBy(x => x.NamespaceSuffix ?? string.Empty);
            foreach (var group in groups)
            {
                normalizer.NormalizeNames(group);
            }
        }
    }
}
