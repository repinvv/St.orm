namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using StormGenerator.Common;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.NovPropConfigs;
    using StormGenerator.Models.DbModels;
    using StormGenerator.Settings;

    internal class ModelConfigPopulation
    {
        private readonly NamePopulationService namePopulationService;

        public ModelConfigPopulation(NamePopulationService namePopulationService)
        {
            this.namePopulationService = namePopulationService;
        }

        public ModelConfig PopulateConfig(Table table, List<Table> tables)
        {
            var config = new ModelConfig
                         {
                             IsEnabled = true,
                             DbTableId = table.Id,
                             Name = namePopulationService.CreateItemName(table.Name),
                             NamespaceSuffix = table.Schema == "dbo"
                                 ? null
                                 : namePopulationService.CreateItemName(table.Schema),
                             Fields = new List<FieldConfig>(),
                             NavProps = new List<NavPropConfig>()
                         };
            return config;
        }
    }
}
