namespace StormGenerator.ModelsCreation
{
    using System;
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.Configs.RelationConfigs.Params;
    using StormGenerator.Models.GenModels;
    using StormGenerator.Models.GenModels.Params;

    internal class RelationCreate
    {
        private readonly RelationParamsCreate relationParamsCreate;

        public RelationCreate(RelationParamsCreate relationParamsCreate)
        {
            this.relationParamsCreate = relationParamsCreate;
        }

        public Relation CreateRelation(RelationConfig config)
        {
            return new Relation
            {
                Name = config.Name,
                IsEnabled = config.IsEnabled
            };
        }

        public void FillRelationParams(Model model,
            Relation relation,
            Dictionary<string, Model> modelsDict,
            Dictionary<Relation, RelationConfig> relationDict)
        {
            var relationConfig = relationDict[relation];
            relation.FarModel = modelsDict[relationConfig.FarModelId];
            relation.Parameters = CreateRelationParameters(model,
                relationConfig.Parameters, 
                relation.FarModel, 
                modelsDict);
        }

        private IRelationParams CreateRelationParameters(Model model, 
            IRelationConfigParams config,
            Model farModel, 
            Dictionary<string, Model> modelsDict)
        {
            switch (config.RelationType)
            {
                case RelationType.OneToMany:
                    return relationParamsCreate.CreateOtmParameters(farModel, (OneToManyConfigParams)config);
                case RelationType.OneToManyFlagged:
                    return relationParamsCreate.CreateOtmfParameters(farModel, (OneToManyFlaggedConfigParams)config);
                case RelationType.ManyToOne:
                    return relationParamsCreate.CreateMtoParameters(model, farModel, (ManyToOneConfigParams)config);
                case RelationType.ManyToMany:
                    return relationParamsCreate.CreateMtmParameters((ManyToManyRelationConfigParams)config, modelsDict);
            }

            throw new Exception("wtf?");
        }
    }
}
