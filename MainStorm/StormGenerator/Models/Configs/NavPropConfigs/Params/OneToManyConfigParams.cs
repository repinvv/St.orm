namespace StormGenerator.Models.Configs.NavPropConfigs.Params
{
    using System.Collections.Generic;

    internal class OneToManyConfigParams : INavPropConfigParams
    {
        public virtual NavPropType NavPropType => NavPropType.OneToMany;

        public NavPropAmount NavPropAmount { get; set; }

        public List<Modifier> Modifiers { get; set; }
    }
}