namespace StormGenerator.Models.Configs.NovPropConfigs.Params
{
    using Newtonsoft.Json;

    internal interface INavPropConfigParams
    {
        [JsonIgnore]
        NavPropType NavPropType { get; }
    }
}
