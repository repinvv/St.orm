namespace StormGenerator.Models.Configs.RelationConfigs.Params
{
    using System.Collections.Generic;

    internal class OneToManyConfigParams : IRelationConfigParams
    {
        public virtual RelationType RelationType => RelationType.OneToMany;

        public string ReverseRelationName { get; set; }

        public RelationAmount RelationAmount { get; set; }

        public List<Modifier> Modifiers { get; set; }
    }
}