namespace StormGenerator.Models.Configs.NovPropConfigs
{
    using System.Collections.Generic;

    internal class NavPropConfig : ItemConfig
    {
        public string Name { get; set; }

        public NavPropType NavPropType { get; set; }

        public string AssociationName { get; set; }

        public List<string> NearFields { get; set; }

        public List<string> FarFields { get; set; }

        public string FarModel { get; set; }
        
        public string ReverseNavPropName { get; set; }

        public NavPropAmount NavPropAmount { get; set; }

        public List<Modifier> Modifiers { get; set; }
    }
}
