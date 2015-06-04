namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Config.Db;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationFieldsCollector
    {
        private readonly RelationsCollector relationsCollector;
        private readonly ManyToManyFieldsCollector manyToManyFieldsCollector;
        private readonly RelationFieldFactory fieldFactory;

        public RelationFieldsCollector(RelationsCollector relationsCollector,
            ManyToManyFieldsCollector manyToManyFieldsCollector,
            RelationFieldFactory fieldFactory)
        {
            this.relationsCollector = relationsCollector;
            this.manyToManyFieldsCollector = manyToManyFieldsCollector;
            this.fieldFactory = fieldFactory;
        }

        public void CollectRelations(List<Model> models, RelationsMode relationsMode)
        {
            if (relationsMode == RelationsMode.ConfigOnly)
            {
                return;
            }

            var relations = relationsCollector.CollectRelations(models);
            var mtmModels = new HashSet<Model>(manyToManyFieldsCollector.CollectManyToManyLinks(relations));

            foreach (var relation in relations.Where(x => !mtmModels.Contains(x.Key)))
            {
                var groups = relation.Value.GroupBy(x => x.Id).Where(x => !mtmModels.Contains(x.First().Model)).ToList();
                
                if (relationsMode == RelationsMode.All || groups.Count() <= GenerationConstants.AutoModelCreation.MaximumOneToManyFields)
                {
                    relation.Key.RelationFields.AddRange(groups.Select(x => fieldFactory.CreateOneToManyField(x.ToList())));
                }

                if (relationsMode == RelationsMode.All || groups.Count() > GenerationConstants.AutoModelCreation.MaximumOneToManyFields)
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
