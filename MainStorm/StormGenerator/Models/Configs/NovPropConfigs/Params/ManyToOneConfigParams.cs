namespace StormGenerator.Models.Configs.NovPropConfigs.Params
{
    using System.Collections.Generic;

    internal class ManyToOneConfigParams : INavPropConfigParams
    {
        public NavPropType NavPropType => NavPropType.ManyToOne;

        public List<string> NearFields { get; set; }

        public List<string> FarFields { get; set; }
    }
}
