namespace StormGenerator.Models.GenModels.Params
{
    using StormGenerator.Models.Configs.NavPropConfigs.Params;

    internal class ManyToManyNavPropParams : OneToManyNavPropParams
    {
        public override NavPropType NavPropType => NavPropType.ManyToMany;

        public Model Mediator { get; set; }

        public NavProp FarEndNavProp { get; set; }
    }
}
