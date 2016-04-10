namespace StormGenerator.Models.Configs.RelationConfigs.Params
{
    using System.Collections.Generic;

    internal class ManyToOneConfigParams : IRelationConfigParams
    {
        public RelationType RelationType => RelationType.ManyToOne;

        public List<string> NearFields { get; set; }

        public List<string> FarFields { get; set; }
    }
}
