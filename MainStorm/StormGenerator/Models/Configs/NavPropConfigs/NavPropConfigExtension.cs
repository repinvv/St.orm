namespace StormGenerator.Models.Configs.NavPropConfigs
{
    using StormGenerator.Models.Configs.NavPropConfigs.Params;

    internal static class NavPropConfigExtension
    {
        public static bool IsManyToOne(this NavPropConfig config)
        {
            return config.Parameters?.NavPropType == NavPropType.ManyToOne;
        }
    }
}
