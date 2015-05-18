namespace StormGenerator.Models.Relation
{
    using System.Collections.Generic;

    public class OneToManyField : RelationField
    {
        public List<MappingField> FarEndJoinFields { get; set; } 
    }
}
