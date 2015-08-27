namespace StormGenerator.Generation.ModelGeneration.ModelPartsGeneration
{
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class FieldUtility
    {
        public string GetUsing(MappingField arg)
        {
            return arg.Type?.Namespace ?? arg.CustomTypeNamespace;
        }

        public string GetRelationFieldType(RelationField field)
        {
            if (field.IsList)
            {
                return $"IList<{field.FieldModel.Name}>";
            }

            return field.FieldModel.Name;
        }
    }
}
