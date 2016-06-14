namespace StormGenerator.AutomaticPopulation.RelationsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.DbModels;
    using StormGenerator.Settings;

    internal class RelationConfigsPopulation
    {
        private readonly ManyToOneRelationsPopulation manyToOneRelationsPopulation;
        private readonly ManyToManyRelationsPopulation manyToManyRelationsPopulation;
        private readonly OneToManyRelationsPopulation oneToManyRelationsPopulation;
        private readonly ConfigListNameNormalizer nameNormalizer;
        private AutomaticPopulationOptions options;

        public RelationConfigsPopulation(ManyToOneRelationsPopulation manyToOneRelationsPopulation,
            ManyToManyRelationsPopulation manyToManyRelationsPopulation,
            OneToManyRelationsPopulation oneToManyRelationsPopulation,
            ConfigListNameNormalizer nameNormalizer,
            PopulationOptionsService populationOptionsService)
        {
            this.manyToOneRelationsPopulation = manyToOneRelationsPopulation;
            this.manyToManyRelationsPopulation = manyToManyRelationsPopulation;
            this.oneToManyRelationsPopulation = oneToManyRelationsPopulation;
            this.nameNormalizer = nameNormalizer;
            options = populationOptionsService.AutomaticPopulationOptions;
        }

        internal void PopulateRelations(List<ModelConfig> newConfigs, List<ModelConfig> configs, List<Table> tables)
        {
            manyToOneRelationsPopulation.PopulateManyToOne(newConfigs, configs, tables);
            var mtoLook = configs
                .SelectMany(x => x.Relations, (m, n) => new Mto { ModelConfig = m, RelationConfig = n })
                .Where(x => x.RelationConfig.IsManyToOne())
                .ToLookup(x => x.RelationConfig.FarModelId);
            foreach (var modelConfig in newConfigs)
            {
                modelConfig.Relations
                    .AddRange(manyToManyRelationsPopulation.GetMtmRelations(modelConfig.Id, mtoLook));
                modelConfig.Relations
                    .AddRange(oneToManyRelationsPopulation.GetOtmRelations(modelConfig.Id, mtoLook));
            }

            foreach (var modelConfig in newConfigs)
            {
                nameNormalizer.NormalizeConfigNames(modelConfig.Relations);
                var nonMtoProps = modelConfig.Relations.Where(x => !x.IsManyToOne()).ToList();
                if (nonMtoProps.Count > options.MaxChildModels)
                {
                    nonMtoProps.ForEach(x => x.IsEnabled = false);
                }
            }
        }
    }
}
