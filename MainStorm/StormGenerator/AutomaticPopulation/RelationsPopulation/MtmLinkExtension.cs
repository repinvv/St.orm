namespace StormGenerator.AutomaticPopulation.RelationsPopulation
{
    using System.Linq;
    using StormGenerator.Models.Configs;
    using StormGenerator.Models.Configs.RelationConfigs;

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
                && modelConfig.Relations.Count == 2
                && modelConfig.Relations.All(x => x.IsManyToOne());
        }
    }
}
