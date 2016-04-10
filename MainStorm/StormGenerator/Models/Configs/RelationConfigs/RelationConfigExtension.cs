namespace StormGenerator.Models.Configs.RelationConfigs
{
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal static class RelationConfigExtension
    {
        public static bool IsManyToOne(this RelationConfig config)
        {
            return config.Parameters?.RelationType == RelationType.ManyToOne;
        }
    }
}
