namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration.RelationFieldInitializers
{
    using System.Linq;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToOneFieldInitializer : IRelationFieldInitializer
    {
        private readonly ObjectStringService objectStringService;

        public ManyToOneFieldInitializer(ObjectStringService objectStringService)
        {
            this.objectStringService = objectStringService;
        }

        public void InitializeRelationField(RelationField field, IStringGenerator stringGenerator)
        {
            var required = field.NearEndFields.ActiveAny(x => x.DbField.IsNullable) ? "Optional" : "Required";

            stringGenerator.AppendLine(".Has" + required + "(x => x." + field.Name + ")");
            var reverseField = field.FieldModel.RelationFields.Active().FirstOrDefault(x => x.AssociationId == field.AssociationId);
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".WithMany(" + reverse + ")");
            var objectString = objectStringService.CreateObjectString(field.NearEndFields.ActiveSelect(x => x.Name), "x", false);
            stringGenerator.AppendLine(".HasForeignKey(x => " + objectString + ");");
        }
    }
}
