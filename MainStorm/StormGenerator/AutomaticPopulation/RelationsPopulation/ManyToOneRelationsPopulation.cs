namespace StormGenerator.AutomaticPopulation.RelationsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.Configs.RelationConfigs.Params;
    using StormGenerator.Models.DbModels;

    internal class ManyToOneRelationsPopulation
    {
        private readonly RelationsCollector relationsCollector;
        private readonly NamePopulation namePopulation;
        private readonly ConfigListNameNormalizer nameNormalizer;

        public ManyToOneRelationsPopulation(RelationsCollector relationsCollector, 
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
                config.Relations = GetManyToOneNavs(tablesDict[config.DbTableId], configDict);
                nameNormalizer.NormalizeConfigNames(config.Relations);
            }
        }

        public List<RelationConfig> GetManyToOneNavs(Table table, Dictionary<string, ModelConfig> configs)
        {
            var relationGroups = relationsCollector.CollectRelations(table).GroupBy(x => x.Id);
            var relations = relationGroups
                .Select(x =>
                {
                    var refConfig = configs[x.First().RefTableId];
                    return new RelationConfig
                           {
                               IsEnabled = true,
                               IsGenerated = true,
                               Name = namePopulation.CreateRelationName(refConfig.Name, false),
                               FarModelId = refConfig.Id,
                               Parameters = new ManyToOneConfigParams
                                            {
                                                NearFields = x.Select(y => y.Column).ToList(),
                                                FarFields = x.Select(y => y.RefColumn).ToList(),
                                            }
                           };
                });
            return relations.ToList();
        }
    }
}
