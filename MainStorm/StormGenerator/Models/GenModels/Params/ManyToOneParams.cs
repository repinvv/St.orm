namespace StormGenerator.Models.GenModels.Params
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class ManyToOneParams : IRelationParams
    {
        public RelationType RelationType => RelationType.ManyToOne;

        public List<Field> NearFields { get; set; }

        public List<Field> FarFields { get; set; }
    }
}
