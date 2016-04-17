namespace StormGenerator.ModelsCreation
{
    using System;
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

            FillRelationParams(models, modelsDict, relationDict, x => relationDict[x].IsManyToOne());
            FillRelationParams(models, modelsDict, relationDict, x => !relationDict[x].IsManyToOne());
            return models;
        }

        private void FillRelationParams(List<Model> models,
            Dictionary<string, Model> modelsDict,
            Dictionary<Relation, RelationConfig> relationDict,
            Func<Relation, bool> criteria)
        {
            foreach (var model in models)
            {
                foreach (var relation in model.Relations.Where(criteria))
                {
                    relationCreate.FillRelationParams(model, relation, modelsDict, relationDict);
                }
            }
        }
    }
}
