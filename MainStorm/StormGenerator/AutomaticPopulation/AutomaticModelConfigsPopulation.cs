namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.AutomaticPopulation.RelationsPopulation;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.DbModels;

    internal class AutomaticModelConfigsPopulation
    {
        private readonly ModelConfigPopulation configPopulation;
        private readonly ConfigListNameNormalizer normalizer;
        private readonly RelationConfigsPopulation relationConfigsPopulation;

        public AutomaticModelConfigsPopulation(ModelConfigPopulation configPopulation,
            ConfigListNameNormalizer normalizer,
            RelationConfigsPopulation relationConfigsPopulation)
        {
            this.configPopulation = configPopulation;
            this.normalizer = normalizer;
            this.relationConfigsPopulation = relationConfigsPopulation;
        }

        public List<ModelConfig> PopulateConfigs(List<ModelConfig> existingConfigs, List<Table> tables)
        {
            var existingConfigsDict = existingConfigs.ToDictionary(x => x.DbTableId);
            var unconfiguredTables = tables.Where(x => !existingConfigsDict.ContainsKey(x.Id));
            var newConfigs = unconfiguredTables
                .Select(x => configPopulation.PopulateConfig(x, tables)).ToList();
            var configs = existingConfigs.Concat(newConfigs).ToList();
            relationConfigsPopulation.PopulateRelations(newConfigs, configs, tables);
            NormalizeNames(configs);
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
