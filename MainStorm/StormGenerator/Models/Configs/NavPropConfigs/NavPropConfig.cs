namespace StormGenerator.Models.Configs.NavPropConfigs
{
    using StormGenerator.Models.Configs.NavPropConfigs.Params;

    internal class NavPropConfig : ItemConfig
    {
        public string AssociationName { get; set; }

        public string FarModel { get; set; }
        
        public string ReverseNavPropName { get; set; }

        public INavPropConfigParams Parameters { get; set; }
    }
}
