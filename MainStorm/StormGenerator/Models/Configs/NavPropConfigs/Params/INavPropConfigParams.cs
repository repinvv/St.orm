namespace StormGenerator.Models.Configs.NavPropConfigs.Params
{
    using Newtonsoft.Json;

    internal interface INavPropConfigParams
    {
        [JsonIgnore]
        NavPropType NavPropType { get; }
    }
}
