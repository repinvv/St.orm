namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs.NovPropConfigs;
    using StormGenerator.Models.Configs.NovPropConfigs.Params;

    internal class OneToManyNavPropsPopulation
    {
        private readonly NamePopulation namePopulation;

        public OneToManyNavPropsPopulation(NamePopulation namePopulation)
        {
            this.namePopulation = namePopulation;
        }

        public IEnumerable<NavPropConfig> GetOtmNavProps(string id, ILookup<string, Mto> mtoLook)
        {
            foreach (var mto in mtoLook[id].Where(x => !x.ModelConfig.IsMtmLink(mtoLook)))
            {
                yield return new NavPropConfig
                             {
                                 IsEnabled = true,
                                 IsGenerated = true,
                                 Name = namePopulation.CreateNavPropName(mto.ModelConfig.Name, true),
                                 AssociationName = mto.NavPropConfig.AssociationName,
                                 FarModel = mto.ModelConfig.Id,
                                 ReverseNavPropName = mto.NavPropConfig.Name,
                                 Parameters = new OneToManyConfigParams
                                              {
                                                  Modifiers = new List<Modifier>(),
                                                  NavPropAmount = NavPropAmount.List
                                              }
                             };
            }
        }
    }
}
