namespace StormGenerator.Models.GenModels.Params
{
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class ManyToManyRelationParams : OneToManyRelationParams
    {
        public override RelationType RelationType => RelationType.ManyToMany;

        public Model Mediator { get; set; }

        public Relation FarEndRelation { get; set; }
    }
}
