namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.DbModels;

    internal class ModelConfigPopulation
    {
        private readonly NamePopulation namePopulation;
        private readonly FieldsConfigPopulation fieldsConfigPopulation;

        public ModelConfigPopulation(NamePopulation namePopulation,
            FieldsConfigPopulation fieldsConfigPopulation)
        {
            this.namePopulation = namePopulation;
            this.fieldsConfigPopulation = fieldsConfigPopulation;
        }

        public ModelConfig PopulateConfig(Table table, List<Table> tables)
        {
            var config = new ModelConfig
                         {
                             IsEnabled = true,
                             IsGenerated = true,
                             DbTableId = table.Id,
                             Name = namePopulation.CreateItemName(table.Name),
                             NamespaceSuffix = table.Schema == "dbo"
                                 ? null
                                 : namePopulation.CreateItemName(table.Schema),
                             Fields = fieldsConfigPopulation.PopulateModelFields(table)
                         };
            return config;
        }
    }
}
