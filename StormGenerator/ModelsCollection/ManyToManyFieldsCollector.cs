namespace StormGenerator.ModelsCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToManyFieldsCollector
    {
        private readonly RelationFieldFactory fieldFactory;

        public ManyToManyFieldsCollector(RelationFieldFactory fieldFactory)
        {
            this.fieldFactory = fieldFactory;
        }

        public List<Model> CollectManyToManyLinks(Dictionary<Model, List<Relation>> relations)
        {
            var mtmModels = new List<Model>();
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
                    fieldFactory.CreateManyToManyField(first, last);
                    fieldFactory.CreateManyToManyField(last, first);
                    mtmModels.Add(mtmModel);
                }
            }

            return mtmModels;
        }
    }
}
