namespace StormGenerator.Models.Relation
{
    internal abstract class RelationField
    {
        public string Name { get; set; }

        public Model FieldModel { get; set; }

        public bool IsList { get; set; }

        public override string ToString()
        {
            return "Field: " + Name + " FieldModel: " + FieldModel.Name;
        }
    }
}
