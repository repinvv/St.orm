namespace StormGenerator.Models.Configs.NavPropConfigs.Params
{
    internal class ManyToManyNavPropConfigParams : OneToManyConfigParams
    {
        public override NavPropType NavPropType => NavPropType.ManyToMany;

        public string MediatorId { get; set; }

        public string FarEndNavPropName { get; set; }
    }
}
