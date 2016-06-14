namespace StormGenerator.ModelsCreation
{
    using System.Collections.Generic;
    using System.Linq;
    using Storm;
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
            var fields = config.Fields
                               .Select(x => CreateField(x, columnsDict))
                               .Where(x => x.IsEnabled || x.IsKeyField())
                               .ToList();
            var keys = fields.Where(x => x.IsKeyField())
                             .ToList();
            var model = new Model
                        {
                            IsEnabled = config.IsEnabled && fields.Count > 0,
                            Name = config.Name,
                            NamespaceSuffix = config.NamespaceSuffix,
                            IsStruct = config.IsStruct,
                            Table = table,
                            Fields = fields,
                            KeyFields = keys,
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
            var column = columnsDict.SafeGet(config.DbColumnName);
            return new Field
            {
                IsEnabled = config.IsEnabled && column != null,
                Name = config.Name,
                Column = column,
            };
        }
    }
}
