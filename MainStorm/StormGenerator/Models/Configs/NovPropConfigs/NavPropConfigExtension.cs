namespace StormGenerator.Models.Configs.NovPropConfigs
{
    using StormGenerator.Models.Configs.NovPropConfigs.Params;

    internal static class NavPropConfigExtension
    {
        public static bool IsManyToOne(this NavPropConfig config)
        {
            return config.Parameters?.NavPropType == NavPropType.ManyToOne;
        }
    }
}
