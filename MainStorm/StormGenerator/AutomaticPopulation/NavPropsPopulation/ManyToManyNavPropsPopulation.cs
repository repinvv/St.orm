namespace StormGenerator.AutomaticPopulation.NavPropsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs.NovPropConfigs;
    using StormGenerator.Models.Configs.NovPropConfigs.Params;

    internal class ManyToManyNavPropsPopulation
    {
        private readonly NamePopulation namePopulation;

        public ManyToManyNavPropsPopulation(NamePopulation namePopulation)
        {
            this.namePopulation = namePopulation;
        }

        public IEnumerable<NavPropConfig> GetMtmNavProps(string id, ILookup<string, Mto> mtoLook)
        {
            foreach (var mto in mtoLook[id].Where(x => x.ModelConfig.IsMtmLink(mtoLook)))
            {
                var farendProp = mto.ModelConfig.NavProps.First(x => x != mto.NavPropConfig);
                yield return new NavPropConfig
                {
                    IsEnabled = true,
                    IsGenerated = true,
                    Name = namePopulation.CreateNavPropName(farendProp.Name, true),
                    AssociationName = mto.NavPropConfig.AssociationName,
                    FarModel = farendProp.FarModel,
                    ReverseNavPropName = mto.NavPropConfig.Name,
                    Parameters = new ManyToManyNavPropConfigParams
                    {
                        MediatorId = mto.ModelConfig.Id,
                        FarEndNavPropName = farendProp.Name,
                        NavPropAmount = NavPropAmount.List,
                        Modifiers = new List<Modifier>(),
                    }
                };
            }
        }
    }
}
