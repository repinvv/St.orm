namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationsModeChecker
    {
        public bool IsOtmRelationNeeded(List<Relation> relations, RelationsMode relationsMode)
        {
            var groups = relations.GroupBy(x => x.Id).Where(x => !x.First().Model.IsManyToManyLink).ToList();
            return groups.Any() &&
                   (relationsMode == RelationsMode.All || groups.Count() <= GenerationConstants.AutoModelCreation.MaximumOneToManyFields);
        }

        public bool IsMtoRelationNeeded(List<Relation> relations, RelationsMode relationsMode)
        {
            var groups = relations.GroupBy(x => x.Id).Where(x => !x.First().Model.IsManyToManyLink);
            var rel = relations.FirstOrDefault();
            if (rel != null && rel.RootModel.Name.ToLower() == "country")
            {
                return true;
            }

            return relationsMode == RelationsMode.All || groups.Count() > GenerationConstants.AutoModelCreation.MaximumOneToManyFields;
        }
    }
}
