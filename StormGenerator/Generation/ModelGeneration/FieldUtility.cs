namespace StormGenerator.Generation.ModelGeneration
{
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class FieldUtility
    {
        public string GetUsing(MappingField arg)
        {
            return arg.Type == null ? arg.CustomTypeNamespace : arg.Type.Namespace;
        }

        public string GetRelationFieldType(RelationField field)
        {
            if (field.IsList)
            {
                return "IList<" + field.FieldModel.Name + ">";
            }

            return field.FieldModel.Name;
        }
    }
}
