namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.NovPropConfigs;
    using StormGenerator.Models.Configs.NovPropConfigs.Params;
    using StormGenerator.Models.DbModels;

    internal class NavPropConfigsPopulation
    {
        private readonly RelationsCollector relationsCollector;
        private readonly NamePopulation namePopulation;

        public NavPropConfigsPopulation(RelationsCollector relationsCollector, NamePopulation namePopulation)
        {
            this.relationsCollector = relationsCollector;
            this.namePopulation = namePopulation;
        }

        public List<NavPropConfig> PopulateNavProps(Table table, Dictionary<string, ModelConfig> configs)
        {
            var manyToOneNavs = GetManyToOneNavs(table, configs);
            return null;
        }

        private List<NavPropConfig> GetManyToOneNavs(Table table, Dictionary<string, ModelConfig> configs)
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
                        NearFields = x.Select(y => y.Column).ToList(),
                        FarFields = x.Select(y => y.RefColumn).ToList(),
                        Parameters = new ManyToOneConfigParams()
                    };
                });
            return navProps.ToList();
        }

        internal void PopulateNavProps(List<ModelConfig> newConfigs, List<ModelConfig> configs, List<Table> tables)
        {
            var configDict = configs.ToDictionary(x => x.DbTableId);
            var tablesDict = tables.ToDictionary(x => x.Id);
            foreach (var config in newConfigs)
            {
                config.NavProps = GetManyToOneNavs(tablesDict[config.DbTableId], configDict);
            }
        }
    }
}
