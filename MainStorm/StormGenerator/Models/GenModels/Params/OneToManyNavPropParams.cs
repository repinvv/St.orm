namespace StormGenerator.Models.GenModels.Params
{
    using System.Collections.Generic;
    using StormGenerator.Models.Configs.NavPropConfigs;
    using StormGenerator.Models.Configs.NavPropConfigs.Params;

    internal class OneToManyNavPropParams : INavPropParams
    {
        public virtual NavPropType NavPropType => NavPropType.OneToMany;

        public NavPropAmount NavPropAmount { get; set; }

        public List<Modifier> Modifiers { get; set; }
    }
}
