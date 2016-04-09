namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.NovPropConfigs;
    using StormGenerator.Models.Configs.NovPropConfigs.Params;
    using StormGenerator.Models.DbModels;
    using StormGenerator.Settings;

    internal class NavPropConfigsPopulation
    {
        private readonly ManyToOneNavPropsPopulation manyToOneNavPropsPopulation;
        private readonly ManyToManyNavPropsPopulation manyToManyNavPropsPopulation;
        private readonly OneToManyNavPropsPopulation oneToManyNavPropsPopulation;
        private readonly ConfigListNameNormalizer nameNormalizer;
        private AutomaticPopulationOptions options;

        public NavPropConfigsPopulation(ManyToOneNavPropsPopulation manyToOneNavPropsPopulation,
            ManyToManyNavPropsPopulation manyToManyNavPropsPopulation,
            OneToManyNavPropsPopulation oneToManyNavPropsPopulation,
            ConfigListNameNormalizer nameNormalizer,
            PopulationOptionsService populationOptionsService)
        {
            this.manyToOneNavPropsPopulation = manyToOneNavPropsPopulation;
            this.manyToManyNavPropsPopulation = manyToManyNavPropsPopulation;
            this.oneToManyNavPropsPopulation = oneToManyNavPropsPopulation;
            this.nameNormalizer = nameNormalizer;
            options = populationOptionsService.AutomaticPopulationOptions;
        }

        internal void PopulateNavProps(List<ModelConfig> newConfigs, List<ModelConfig> configs, List<Table> tables)
        {
            manyToOneNavPropsPopulation.PopulateManyToOne(newConfigs, configs, tables);
            var mtoLook = configs
                .SelectMany(x => x.NavProps, (m, n) => new Mto { ModelConfig = m, NavPropConfig = n })
                .Where(x => x.NavPropConfig.IsManyToOne())
                .ToLookup(x => x.NavPropConfig.FarModel);
            foreach (var modelConfig in newConfigs)
            {
                modelConfig.NavProps
                    .AddRange(manyToManyNavPropsPopulation.GetMtmNavProps(modelConfig.Id, mtoLook));
                modelConfig.NavProps
                    .AddRange(oneToManyNavPropsPopulation.GetOtmNavProps(modelConfig.Id, mtoLook));
            }

            foreach (var modelConfig in newConfigs)
            {
                nameNormalizer.NormalizeNames(modelConfig.NavProps);
                var nonMtoProps = modelConfig.NavProps.Where(x => !x.IsManyToOne()).ToList();
                if (nonMtoProps.Count > options.MaxChildModels)
                {
                    nonMtoProps.ForEach(x => x.IsEnabled = false);
                }
            }
        }
    }
}
