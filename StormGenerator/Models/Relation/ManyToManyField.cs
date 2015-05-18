namespace StormGenerator.Models.Relation
{
    public class ManyToManyField : RelationField
    {
        public Model MediatorModel { get; set; }

        public MappingField NearEndJoinField { get; set; }

        public MappingField FarEndJoinField { get; set; }
    }
}
