namespace StormGenerator.Models.Relation
{
    using System.Collections.Generic;

    public class JoinField : RelationField
    {
        public List<MappingField> NearEndJoinFields { get; set; }

        public List<MappingField> FarEndJoinFields { get; set; }
    }
}
