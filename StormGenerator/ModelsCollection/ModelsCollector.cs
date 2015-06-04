namespace StormGenerator.ModelsCollection
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Config.Db;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ModelsCollector
    {
        private readonly NameCreator nameCreator;
        private readonly FieldTypeService fieldTypeService;
        private readonly RelationFieldsCollector relationFieldsCollector;

        public ModelsCollector(NameCreator nameCreator, FieldTypeService fieldTypeService, RelationFieldsCollector relationFieldsCollector)
        {
            this.nameCreator = nameCreator;
            this.fieldTypeService = fieldTypeService;
            this.relationFieldsCollector = relationFieldsCollector;
        }

        public List<Model> CollectModels(StormConfig stormConfig)
        {
            var list = stormConfig.DbModels.Select(CreateModel).ToList();
            relationFieldsCollector.CollectRelations(list, stormConfig.RelationsMode);
            return list;
        }

        private Model CreateModel(DbModel arg)
        {
            return new Model
                   {
                       Name = nameCreator.CreateCamelCaseName(arg.Name),
                       DbModel = arg,
                       MappingFields = arg.Fields.OrderBy(x => x.Index).Select(CreateMappingField).ToList(),
                       RelationFields = new List<RelationField>()
                   };
        }

        private MappingField CreateMappingField(DbField field)
        {
            return new MappingField
                   {
                       Name = nameCreator.CreateCamelCaseName(field.Name),
                       Type = fieldTypeService.GetFieldType(field.Type, field.IsNullable),
                       DbField = field
                   };
        }
    }
}
