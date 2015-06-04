namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationsCollector
    {
        public Dictionary<Model, List<Relation>> CollectRelations(List<Model> list)
        {
            var modelDict = list.ToDictionary(x => x.DbModel.Id);
            var relDict = list.ToDictionary(x => x, x => new List<Relation>());

            foreach (var model in list)
            {
                foreach (var field in model.MappingFields)
                {
                    foreach (var association in field.DbField.Associations)
                    {
                        var relmodel = modelDict[association.TableId];
                        relDict[relmodel]
                            .Add(new Relation
                                 {
                                     RootModel = relmodel,
                                     Model = model,
                                     Field = field,
                                     RootField = relmodel.MappingFields.First(x => x.DbField.Name == association.FieldName),
                                     Id = association.ConstraintId,
                                     Index = association.Index
                                 });
                    }
                }
            }

            return relDict;
        }
    }
}
