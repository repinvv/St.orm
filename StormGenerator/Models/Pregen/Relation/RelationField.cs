namespace StormGenerator.Models.Pregen.Relation
{
    using System.Collections.Generic;

    internal abstract class RelationField : ModelBase
    {
        public bool Cascade { get; set; }

        public string AssociationId { get; set; }

        public Model FieldModel { get; set; }

        public List<MappingField> NearEndFields { get; set; }

        public List<MappingField> FarEndFields { get; set; } 

        public bool IsList { get; set; }

        public override string ToString()
        {
            return "Field: " + Name + " FieldModel: " + FieldModel.Name;
        }
    }
}
