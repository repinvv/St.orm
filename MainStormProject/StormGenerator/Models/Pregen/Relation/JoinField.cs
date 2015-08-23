namespace StormGenerator.Models.Pregen.Relation
{
    using System.Collections.Generic;

    internal class JoinField : RelationField
    {
        public List<MappingField> NearEndJoinFields { get; set; }

        public List<MappingField> FarEndJoinFields { get; set; }
    }
}
