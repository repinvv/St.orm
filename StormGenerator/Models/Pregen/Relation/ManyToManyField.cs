namespace StormGenerator.Models.Pregen.Relation
{
    internal class ManyToManyField : RelationField
    {
        public Model MediatorModel { get; set; }

        public MappingField NearEndJoinField { get; set; }

        public MappingField FarEndJoinField { get; set; }
    }
}
