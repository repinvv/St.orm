namespace StormGenerator.Models.Relation
{
    using System.Collections.Generic;

    internal class OneToManyField : RelationField
    {
        public List<MappingField> FarEndJoinFields { get; set; } 
    }
}
