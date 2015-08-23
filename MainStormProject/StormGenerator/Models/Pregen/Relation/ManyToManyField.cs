namespace StormGenerator.Models.Pregen.Relation
{
    internal class ManyToManyField : RelationField
    {
        public Model MediatorModel { get; set; }

        public RelationField MediatorMtoField { get; set; }
    }
}
