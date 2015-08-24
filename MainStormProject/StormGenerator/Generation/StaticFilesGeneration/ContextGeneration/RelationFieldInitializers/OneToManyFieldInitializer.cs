namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration.RelationFieldInitializers
{
    using System.Linq;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class OneToManyFieldInitializer : IRelationFieldInitializer
    {
        private readonly ObjectStringService objectStringService;

        public OneToManyFieldInitializer(ObjectStringService objectStringService)
        {
            this.objectStringService = objectStringService;
        }

        public void InitializeRelationField(RelationField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(".HasMany(x => x." + field.Name + ")");
            var reverseField = field.FieldModel.RelationFields.Active().FirstOrDefault(x => x.AssociationId == field.AssociationId);
            var required = field.FarEndFields.ActiveAny(x => x.DbField.IsNullable) ? "Optional" : "Required";
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".With" + required + "(" + reverse + ")");
            var objectString = objectStringService.CreateObjectString(field.FarEndFields.ActiveSelect(x => x.Name), "x", false);
            stringGenerator.AppendLine(".HasForeignKey(x => " + objectString + ");");
        }
    }
}
