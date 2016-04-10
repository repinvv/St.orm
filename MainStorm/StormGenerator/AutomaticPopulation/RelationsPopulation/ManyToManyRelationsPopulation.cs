namespace StormGenerator.AutomaticPopulation.RelationsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class ManyToManyRelationsPopulation
    {
        private readonly NamePopulation namePopulation;

        public ManyToManyRelationsPopulation(NamePopulation namePopulation)
        {
            this.namePopulation = namePopulation;
        }

        public IEnumerable<RelationConfig> GetMtmRelations(string id, ILookup<string, Mto> mtoLook)
        {
            foreach (var mto in mtoLook[id].Where(x => x.ModelConfig.IsMtmLink(mtoLook)))
            {
                var farendProp = mto.ModelConfig.Relations.First(x => x != mto.RelationConfig);
                yield return new RelationConfig
                {
                    IsEnabled = true,
                    IsGenerated = true,
                    Name = namePopulation.CreateRelationName(farendProp.Name, true),
                    FarModelId = farendProp.FarModelId,
                    Parameters = new ManyToManyRelationConfigParams
                    {
                        MediatorId = mto.ModelConfig.Id,
                        ReverseRelationName = mto.RelationConfig.Name,
                        FarEndRelationName = farendProp.Name,
                        RelationAmount = RelationAmount.List,
                        Modifiers = new List<Modifier>(),
                    }
                };
            }
        }
    }
}
