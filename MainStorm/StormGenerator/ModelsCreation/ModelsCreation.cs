namespace StormGenerator.ModelsCreation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.GenModels;

    internal class ModelsCreation
    {
        private readonly ModelCreation modelCreation;
        private readonly RelationCreate relationCreate;

        public ModelsCreation(ModelCreation modelCreation, RelationCreate relationCreate)
        {
            this.modelCreation = modelCreation;
            this.relationCreate = relationCreate;
        }

        public List<Model> CreateModelsFromSchema(Schema schema)
        {
            var tablesDict = schema.Tables.ToDictionary(x => x.Id);
            var relationDict = new Dictionary<Relation, RelationConfig>();
            var models = schema.Configs.Select(x => modelCreation.CreateModel(x, tablesDict, relationDict)).ToList();
            var modelsDict = models.ToDictionary(x => x.Id);
            foreach (var relation in models.SelectMany(x=>x.Relations))
            {
                relationCreate.FillRelationParams(relation, modelsDict);
            }
            return models;
        }
    }
}
