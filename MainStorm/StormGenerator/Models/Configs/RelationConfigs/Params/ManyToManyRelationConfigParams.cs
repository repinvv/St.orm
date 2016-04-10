namespace StormGenerator.Models.Configs.RelationConfigs.Params
{
    internal class ManyToManyRelationConfigParams : OneToManyConfigParams
    {
        public override RelationType RelationType => RelationType.ManyToMany;

        public string MediatorId { get; set; }

        public string FarEndRelationName { get; set; }
    }
}
