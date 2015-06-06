namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationsModeChecker
    {
        public bool IsOtmRelationNeeded(List<Relation> relations, HashSet<Model> mtmModels, RelationsMode relationsMode)
        {
            var groups = relations.GroupBy(x => x.Id).Where(x => !mtmModels.Contains(x.First().Model));
            return relations.Any() &&
                   (relationsMode == RelationsMode.All || groups.Count() <= GenerationConstants.AutoModelCreation.MaximumOneToManyFields);
        }

        public bool IsMtoRelationNeeded(List<Relation> relations, HashSet<Model> mtmModels, RelationsMode relationsMode)
        {
            var groups = relations.GroupBy(x => x.Id).Where(x => !mtmModels.Contains(x.First().Model));
            return relationsMode == RelationsMode.All || groups.Count() > GenerationConstants.AutoModelCreation.MaximumOneToManyFields;
        }
    }
}
