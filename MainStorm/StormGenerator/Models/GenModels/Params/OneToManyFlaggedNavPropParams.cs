namespace StormGenerator.Models.GenModels.Params
{
    using StormGenerator.Models.Configs.NavPropConfigs.Params;

    internal class OneToManyFlaggedNavPropParams : INavPropParams
    {
        public NavPropType NavPropType => NavPropType.OneToManyFlagged;

        public string FlagColumn { get; set; }

        public string TrueValue { get; set; }

        public string FalseValue { get; set; }
    }
}
