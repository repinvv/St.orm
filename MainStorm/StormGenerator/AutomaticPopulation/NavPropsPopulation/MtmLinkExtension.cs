namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.NovPropConfigs;

    internal static class MtmLinkExtension
    {
        public static bool IsMtmLink(this ModelConfig modelConfig, ILookup<string, Mto> mtoLook)
        {
            return !mtoLook[modelConfig.Id].Any()
                   && modelConfig.IsMtmLink();
        }

        public static bool IsMtmLink(this ModelConfig modelConfig)
        {
            return modelConfig.Fields.Count == 2
                && modelConfig.NavProps.Count == 2
                && modelConfig.NavProps.All(x => x.IsManyToOne());
        }
    }
}
