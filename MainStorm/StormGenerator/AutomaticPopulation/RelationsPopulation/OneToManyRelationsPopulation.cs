namespace StormGenerator.AutomaticPopulation.RelationsPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Configs.RelationConfigs;
    using StormGenerator.Models.Configs.RelationConfigs.Params;

    internal class OneToManyRelationsPopulation
    {
        private readonly NamePopulation namePopulation;

        public OneToManyRelationsPopulation(NamePopulation namePopulation)
        {
            this.namePopulation = namePopulation;
        }

        public IEnumerable<RelationConfig> GetOtmRelations(string id, ILookup<string, Mto> mtoLook)
        {
            foreach (var mto in mtoLook[id].Where(x => !x.ModelConfig.IsMtmLink(mtoLook)))
            {
                yield return new RelationConfig
                             {
                                 IsEnabled = true,
                                 IsGenerated = true,
                                 Name = namePopulation.CreateRelationName(mto.ModelConfig.Name, true),
                                 FarModelId = mto.ModelConfig.Id,
                                 Parameters = new OneToManyConfigParams
                                              {
                                                  ReverseRelationName = mto.RelationConfig.Name,
                                                  Modifiers = new List<Modifier>(),
                                                  RelationAmount = RelationAmount.List
                                              }
                             };
            }
        }
    }
}
