namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Pregen;

    internal class RelationFieldsCollector
    {
        private readonly RelationsCollector relationsCollector;
        private readonly ManyToManyFieldsCollector manyToManyFieldsCollector;
        private readonly RelationFieldFactory fieldFactory;
        private readonly RelationsModeChecker modeChecker;

        public RelationFieldsCollector(RelationsCollector relationsCollector,
            ManyToManyFieldsCollector manyToManyFieldsCollector,
            RelationFieldFactory fieldFactory,
            RelationsModeChecker modeChecker)
        {
            this.relationsCollector = relationsCollector;
            this.manyToManyFieldsCollector = manyToManyFieldsCollector;
            this.fieldFactory = fieldFactory;
            this.modeChecker = modeChecker;
        }

        public void CollectRelations(List<Model> models, RelationsMode relationsMode)
        {
            if (relationsMode == RelationsMode.ConfigOnly)
            {
                return;
            }

            var relations = relationsCollector.CollectRelations(models);
            manyToManyFieldsCollector.CollectManyToManyLinks(relations, relationsMode);

            foreach (var relation in relations.Where(x => !x.Key.IsManyToManyLink))
            {
                var groups = relation.Value.GroupBy(x => x.Id).Where(x => !x.First().Model.IsManyToManyLink).ToList();
                if (modeChecker.IsOtmRelationNeeded(relation.Value, relationsMode))
                {
                    relation.Key.RelationFields.AddRange(groups.Select(x => fieldFactory.CreateOneToManyField(x.ToList())));
                }

                if (modeChecker.IsMtoRelationNeeded(relation.Value, relationsMode))
                {
                    foreach (var group in groups)
                    {
                        var relationsList = group.ToList();
                        relationsList[0].Model.RelationFields.Add(fieldFactory.CreateManyToOneField(relationsList));
                    }
                }
            }
        }
    }
}
