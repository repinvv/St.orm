namespace StormGenerator.ModelsCreation
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.GenModels;

    internal class RelationCreate
    {
        public Relation CreateRelation(RelationConfig config)
        {
            return new Relation
            {
                Name = config.Name,
                IsEnabled = config.IsEnabled
            };
        }

        public void FillRelationParams(Relation relation, Dictionary<string, Model> modelsDict)
        {
            
        }
    }
}
