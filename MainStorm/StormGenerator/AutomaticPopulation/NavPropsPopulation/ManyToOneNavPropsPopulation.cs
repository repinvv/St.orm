namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.NavPropConfigs;
    using StormGenerator.Models.Configs.NavPropConfigs.Params;
    using StormGenerator.Models.DbModels;

    internal class ManyToOneNavPropsPopulation
    {
        private readonly RelationsCollector relationsCollector;
        private readonly NamePopulation namePopulation;
        private readonly ConfigListNameNormalizer nameNormalizer;

        public ManyToOneNavPropsPopulation(RelationsCollector relationsCollector, 
            NamePopulation namePopulation, 
            ConfigListNameNormalizer nameNormalizer)
        {
            this.relationsCollector = relationsCollector;
            this.namePopulation = namePopulation;
            this.nameNormalizer = nameNormalizer;
        }

        public void PopulateManyToOne(List<ModelConfig> newConfigs, List<ModelConfig> configs, List<Table> tables)
        {
            var configDict = configs.ToDictionary(x => x.DbTableId);
            var tablesDict = tables.ToDictionary(x => x.Id);
            foreach (var config in newConfigs)
            {
                config.NavProps = GetManyToOneNavs(tablesDict[config.DbTableId], configDict);
                nameNormalizer.NormalizeNames(config.NavProps);
            }
        }

        public List<NavPropConfig> GetManyToOneNavs(Table table, Dictionary<string, ModelConfig> configs)
        {
            var relations = relationsCollector.CollectRelations(table).GroupBy(x => x.Id);
            var navProps = relations
                .Select(x =>
                {
                    var refConfig = configs[x.First().RefTableId];
                    return new NavPropConfig
                           {
                               IsEnabled = true,
                               IsGenerated = true,
                               Name = namePopulation.CreateNavPropName(refConfig.Name, false),
                               AssociationName = x.Key,
                               FarModel = refConfig.Id,
                               Parameters = new ManyToOneConfigParams
                                            {
                                                NearFields = x.Select(y => y.Column).ToList(),
                                                FarFields = x.Select(y => y.RefColumn).ToList(),
                                            }
                           };
                });
            return navProps.ToList();
        }
    }
}
