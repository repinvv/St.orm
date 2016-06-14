namespace StormGenerator.ModelsCreation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.AutomaticPopulation;
    using StormGenerator.Models;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.GenModels;

    internal class ModelsCreation
    {
        private readonly ModelCreation modelCreation;
        private readonly RelationCreate relationCreate;
        private readonly NamePopulation namePopulation;
        private readonly ConfigListNameNormalizer nameNormalizer;

        public ModelsCreation(ModelCreation modelCreation, 
            RelationCreate relationCreate,
            NamePopulation namePopulation,
            ConfigListNameNormalizer nameNormalizer)
        {
            this.modelCreation = modelCreation;
            this.relationCreate = relationCreate;
            this.namePopulation = namePopulation;
            this.nameNormalizer = nameNormalizer;
        }

        public List<EntityModel> CreateModelsFromSchema(Schema schema)
        {
            var tablesDict = schema.Tables.ToDictionary(x => x.Id);
            var relationDict = new Dictionary<Relation, RelationConfig>();
            var models = schema.Configs.Select(x => modelCreation.CreateModel(x, tablesDict, relationDict)).ToList();
            var modelsDict = models.ToDictionary(x => x.Id);

            FillRelationParams(models, modelsDict, relationDict, x => relationDict[x].IsManyToOne());
            FillRelationParams(models, modelsDict, relationDict, x => !relationDict[x].IsManyToOne());
            var list = models.Select(x => new EntityModel
                                      {
                                          Model = x,
                                          Name = namePopulation.CreateRelationName(x.Name, true)
                                      })
                         .ToList();
            nameNormalizer.NormalizeNames(list);
            return list;
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
