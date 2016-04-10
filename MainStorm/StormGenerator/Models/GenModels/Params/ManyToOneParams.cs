namespace StormGenerator.Models.GenModels.Params
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.NavPropConfigs.Params;

    internal class ManyToOneParams : INavPropParams
    {
        public NavPropType NavPropType => NavPropType.ManyToOne;

        public List<Field> NearFields { get; set; }

        public List<Field> FarFields { get; set; }
    }
}
