namespace StormGenerator.Models.Configs.RelationConfigs
{
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class RelationConfig : ItemConfig
    {
        public string FarModelId { get; set; }

        public IRelationConfigParams Parameters { get; set; }
    }
}
