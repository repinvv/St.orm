namespace StormGenerator.Models.Pregen.Relation
{
    using System.Collections.Generic;

    internal class ManyToOneField : RelationField
    {
        public List<MappingField> NearEndJoinFields { get; set; }
    }
}
