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

            foreach (var model in models)
            {
                foreach (var relation in model.Relations)
                {
                    relationCreate.CreateMtoRelationParams(model, relation, modelsDict, relationDict);
                }
            }

            foreach (var model in models)
            {
                foreach (var relation in model.Relations.Where(x => x.Parameters == null))
                {
                    relationCreate.FillRelationParams(model, relation, modelsDict, relationDict);
                }
            }

            return models;
        }
    }
}
