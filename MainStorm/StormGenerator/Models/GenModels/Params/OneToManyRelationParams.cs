namespace StormGenerator.Models.GenModels.Params
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class OneToManyRelationParams : IRelationParams
    {
        public virtual RelationType RelationType => RelationType.OneToMany;

        public Relation ReverseRelation { get; set; }

        public RelationAmount RelationAmount { get; set; }

        public List<Modifier> Modifiers { get; set; }
    }
}
