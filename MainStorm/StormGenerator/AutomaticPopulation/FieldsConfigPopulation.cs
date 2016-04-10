namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.DbModels;

    internal class FieldsConfigPopulation
    {
        private readonly NamePopulation namePopulation;
        private readonly ConfigListNameNormalizer normalizer;

        public FieldsConfigPopulation(NamePopulation namePopulation, ConfigListNameNormalizer normalizer)
        {
            this.namePopulation = namePopulation;
            this.normalizer = normalizer;
        }

        public List<FieldConfig> PopulateModelFields(Table table)
        {
            var fields = table.Columns.Select(CreateFieldConfig).ToList();
            normalizer.NormalizeNames(fields);
            return fields;
        }

        private FieldConfig CreateFieldConfig(Column column)
        {
            return new FieldConfig
                   {
                       Name = namePopulation.CreateItemName(column.Name),
                       IsGenerated = true,
                       IsEnabled = true,
                       DbColumnName = column.Name,
                   };
        }
    }
}
