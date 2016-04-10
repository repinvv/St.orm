namespace StormGenerator.ModelsCreation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.DbModels;
    using StormGenerator.Models.GenModels;

    internal class ModelCreation
    {
        private readonly RelationCreate relationCreate;

        public ModelCreation(RelationCreate relationCreate)
        {
            this.relationCreate = relationCreate;
        }

        public Model CreateModel(ModelConfig config,
            Dictionary<string, Table> tablesDict,
            Dictionary<Relation, RelationConfig> relationDict)
        {
            var table = tablesDict[config.DbTableId];
            var columnsDict = table.Columns.ToDictionary(x => x.Name);
            var model = new Model
            {
                IsEnabled = config.IsEnabled,
                Name = config.Name,
                NamespaceSuffix = config.NamespaceSuffix,
                Table = table,
                Fields = config.Fields.Select(x => CreateField(x, columnsDict)).ToList(),
                Relations = new List<Relation>()
            };
            foreach (var relationConfig in config.Relations)
            {
                var relation = relationCreate.CreateRelation(relationConfig);
                relationDict.Add(relation, relationConfig);
                model.Relations.Add(relation);
            }
            return model;
        }

        private Field CreateField(FieldConfig config, Dictionary<string, Column> columnsDict)
        {
            var column = columnsDict[config.DbColumnName];
            return new Field
            {
                IsEnabled = config.IsEnabled,
                Name = config.Name,
                Column = column,
            };
        }
    }
}
