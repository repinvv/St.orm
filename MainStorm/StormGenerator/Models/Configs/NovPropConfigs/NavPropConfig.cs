namespace StormGenerator.Models.Configs.NovPropConfigs
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.NovPropConfigs.Params;

    internal class NavPropConfig : ItemConfig
    {
        public string AssociationName { get; set; }

        public string FarModel { get; set; }
        
        public string ReverseNavPropName { get; set; }

        public INavPropConfigParams Parameters { get; set; }
    }
}
