namespace StormGenerator.ModelsCreation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs.RelationConfigs.Params;
    using StormGenerator.Models.GenModels;
    using StormGenerator.Models.GenModels.Params;

    internal class RelationParamsCreate
    {
        public IRelationParams CreateOtmParameters(Model farModel, OneToManyConfigParams otmConfig)
        {
            return new OneToManyRelationParams
            {
                Modifiers = otmConfig.Modifiers,
                RelationAmount = otmConfig.RelationAmount,
                ReverseRelation = GetRelation(farModel, otmConfig.ReverseRelationName)
            };
        }

        public IRelationParams CreateOtmfParameters(Model farModel, OneToManyFlaggedConfigParams otmfConfig)
        {
            return new OneToManyFlaggedRelationParams
            {
                ReverseRelation = GetRelation(farModel, otmfConfig.ReverseRelationName),
                FlagColumn = otmfConfig.FlagColumn,
                TrueValue = otmfConfig.TrueValue,
                FalseValue = otmfConfig.FalseValue
            };
        }

        public IRelationParams CreateMtoParameters(Model model, Model farModel, ManyToOneConfigParams mtoConfig)
        {
            return new ManyToOneParams
            {
                FarFields = GetFields(farModel, mtoConfig.FarFields),
                NearFields = GetFields(model, mtoConfig.NearFields)
            };
        }

        public IRelationParams CreateMtmParameters(ManyToManyRelationConfigParams config,
            Dictionary<string, Model> modelsDict)
        {
            var mediator = modelsDict[config.MediatorId];
            
            return new ManyToManyRelationParams
                   {
                      Modifiers = config.Modifiers,
                      RelationAmount = config.RelationAmount,
                      Mediator = mediator,
                      ReverseRelation = GetRelation(mediator, config.ReverseRelationName),
                      FarEndRelation = GetRelation(mediator, config.FarEndRelationName)
                   };
        }

        private List<Field> GetFields(Model model, List<string> fieldNames)
        {
            var fieldsDict = model.Fields.ToDictionary(x => x.Column.Name);
            return fieldNames.Select(x => fieldsDict[x]).ToList();
        }

        private Relation GetRelation(Model model, string relationName)
        {
            return model.Relations
                        .Where(x => x.Parameters?.RelationType == RelationType.ManyToOne)
                        .First(x => x.Name == relationName);
        }
    }
}
