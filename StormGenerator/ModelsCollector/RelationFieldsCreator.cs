namespace StormGenerator.ModelsCollector
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models;
    using StormGenerator.Models.Db;
    using StormGenerator.Models.Relation;

    internal class RelationFieldsCreator
    {
        private readonly NameCreator nameCreator;

        public RelationFieldsCreator(NameCreator nameCreator)
        {
            this.nameCreator = nameCreator;
        }

        public void PopulateRelations(List<Model> models)
        {
            var modelDict = models.ToDictionary(x => x.DbModel);
            foreach (var model in models)
            {
                PopulateOneToMany(model, modelDict);
            }

            foreach (var model in models.Where(x => x.RelationFields.Any()))
            {
                PopulateManyToMany(model, models, modelDict);
            }
        }

        private void PopulateOneToMany(Model model, Dictionary<DbModel, Model> modelDict)
        {
            if (model.DbModel.Associations.Count(x => !x.Dependent.IsManyToManyLink) < GenerationConstants.AutoModelCreation.MaximumOneToManyFields)
            {
                var fields = model.DbModel.Associations.Where(x => !x.Dependent.IsManyToManyLink)
                                  .Select(x => CreateOneToMany(x, modelDict[x.Dependent]));
                model.RelationFields.AddRange(fields);
            }
        }

        private RelationField CreateOneToMany(DbAssociation arg, Model model)
        {
            return new OneToManyField
                   {
                       Name = nameCreator.CreatePluralName(model.Name),
                       FieldModel = model,
                       IsList = true,
                       FarEndJoinFields = arg.ReferenceFields.Select(x => model.MappingFields.First(y => y.DbField == x)).ToList()
                   };
        }

        private void PopulateManyToMany(Model model, List<Model> models, Dictionary<DbModel, Model> modelDict)
        {
            foreach (var association in model.DbModel.Associations.Where(x => x.Dependent.IsManyToManyLink))
            {
                var farend = models.First(x => x.DbModel
                                                .Associations.Where(y => y.Dependent.IsManyToManyLink)
                                                .Any(y => y.Dependent == association.Dependent));
                if (farend.RelationFields.Any())
                {
                    continue;
                }

                var mediator = modelDict[association.Dependent];
                var farendAssociation = model.DbModel.Associations.First(y => y.Dependent == association.Dependent);
                var mtm = new ManyToManyField
                          {
                              Name = nameCreator.CreatePluralName(farend.Name),
                              IsList = true,
                              FieldModel = farend,
                              MediatorModel = mediator,
                              NearEndJoinField = mediator.MappingFields.First(x => association.ReferenceFields[0] == x.DbField),
                              FarEndJoinField = mediator.MappingFields.First(x => farendAssociation.ReferenceFields[0] == x.DbField)
                          };
                model.RelationFields.Add(mtm);
            }
        }
    }
}
