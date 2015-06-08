namespace StormGenerator.ModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToManyFieldsCollector
    {
        private readonly RelationFieldFactory fieldFactory;
        private readonly RelationsModeChecker modeChecker;

        public ManyToManyFieldsCollector(RelationFieldFactory fieldFactory, RelationsModeChecker modeChecker)
        {
            this.fieldFactory = fieldFactory;
            this.modeChecker = modeChecker;
        }

        public void CollectManyToManyLinks(Dictionary<Model, List<Relation>> relations, RelationsMode relationsMode)
        {
            var mtmLinks = new List<Tuple<Relation, Relation>>();
            var groupedRelations = relations.SelectMany(x => x.Value)
                                            .GroupBy(x => x.Id)
                                            .Where(x => x.Count() == 1)
                                            .Select(x => x.First());
            var mtmGroups = groupedRelations
                .GroupBy(x => x.Model)
                .Where(x => x.Count() == 2);
            foreach (var mtmGroup in mtmGroups)
            {
                var list = mtmGroup.ToList();
                var first = list[0];
                var last = list[1];
                var mtmModel = first.Model;
                var keys = mtmModel.MappingFields.Where(x => x.DbField.IsPrimaryKey).ToList();
                var restOfFields = mtmModel.MappingFields.Where(x => !x.DbField.IsPrimaryKey).ToList();
                if (keys.Count == 2 && keys.All(x => x.DbField.Associations.Any()) && 
                    restOfFields.Count <= 2 && restOfFields.All(x => x.Type == typeof(DateTime)) && first.RootModel != last.RootModel)
                {
                    mtmLinks.Add(new Tuple<Relation, Relation>(first, last));
                    mtmModel.IsManyToManyLink = true;
                }
            }

            foreach (var mtmModel in mtmLinks)
            {
                if (modeChecker.IsOtmRelationNeeded(relations[mtmModel.Item1.RootModel], relationsMode))
                {
                    fieldFactory.CreateManyToManyField(mtmModel.Item1, mtmModel.Item2);
                }

                if (modeChecker.IsOtmRelationNeeded(relations[mtmModel.Item2.RootModel], relationsMode))
                {
                    fieldFactory.CreateManyToManyField(mtmModel.Item2, mtmModel.Item1);
                }
            }
        }
    }
}
