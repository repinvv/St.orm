namespace StormGenerator.Models.Configs.NovPropConfigs.Params
{
    internal class OneToManyFlaggedConfigParams : INavPropConfigParams
    {
        public NavPropType NavPropType => NavPropType.OneToManyFlagged;

        public string FlagColumn { get; set; }

        public string TrueValue { get; set; }

        public string FalseValue { get; set; }
    }
}
