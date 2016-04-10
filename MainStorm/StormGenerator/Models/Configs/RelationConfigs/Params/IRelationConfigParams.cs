namespace StormGenerator.Models.Configs.RelationConfigs.Params
{
    using Newtonsoft.Json;

    internal interface IRelationConfigParams
    {
        [JsonIgnore]
        RelationType RelationType { get; }
    }
}
