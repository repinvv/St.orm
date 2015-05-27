namespace StormGenerator.ModelsCollector
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models;
    using StormGenerator.Models.Config.Db;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ModelsCollector
    {
        private readonly NameCreator nameCreator;
        private readonly RelationFieldsCreator relationFieldsCreator;

        public ModelsCollector(NameCreator nameCreator, RelationFieldsCreator relationFieldsCreator)
        {
            this.nameCreator = nameCreator;
            this.relationFieldsCreator = relationFieldsCreator;
        }

        public List<Model> CollectModelsWithSettings(List<DbModel> dbmodels, Options settingsFile)
        {
            var models = dbmodels.Select(CreateModel).ToList();
            relationFieldsCreator.PopulateRelations(models);
            return models;
        }

        private Model CreateModel(DbModel dbmodel)
        {
            return new Model
                   {
                       Name = nameCreator.CreateCamelCaseName(dbmodel.Name),
                       MappingFields = dbmodel.Fields.Select(CreateMappingField).ToList(),
                       DbModel = dbmodel,
                       RelationFields = new List<RelationField>()
                   };
        }

        private MappingField CreateMappingField(DbField arg)
        {
            return new MappingField
                   {
                       Name = nameCreator.CreateCamelCaseName(arg.Name),
                       Type = arg.Type,
                       DbField = arg
                   };
        }
    }
}
