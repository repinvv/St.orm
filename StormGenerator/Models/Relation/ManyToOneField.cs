namespace StormGenerator.Models.Relation
{
    using System.Collections.Generic;

    public class ManyToOneField : RelationField
    {
        public List<MappingField> NearEndJoinFields { get; set; }
    }
}
