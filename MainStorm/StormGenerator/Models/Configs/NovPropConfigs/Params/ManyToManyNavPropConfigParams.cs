namespace StormGenerator.Models.Configs.NovPropConfigs.Params
{
    internal class ManyToManyNavPropConfigParams : OneToManyConfigParams
    {
        public override NavPropType NavPropType => NavPropType.ManyToMany;

        public string MediatorId { get; set; }

        public string FarEndNavPropName { get; set; }
    }
}
