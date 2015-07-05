namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class InitializersGenerator
    {
        private readonly ObjectStringService objectStringService;
        private readonly HashSet<string> relationIds = new HashSet<string>();

        public InitializersGenerator(ObjectStringService objectStringService)
        {
            this.objectStringService = objectStringService;
        }

        private bool NeedsInitializer(MappingField field)
        {
            return field.Enabled && GenerationConstants.ModelGeneration.DottedTypes.Contains(field.Type);
        }

        public bool NeedsInitializer(Model model)
        {
            return model.RelationFields.ActiveAny() || model.MappingFields.Any(NeedsInitializer);
        }

        public void GenerateInitializers(List<Model> models, IStringGenerator stringGenerator)
        {
            foreach (var model in models.Where(NeedsInitializer))
            {
                stringGenerator.AppendLine();
                stringGenerator.AppendLine("protected virtual void Initialize" + model.Name + "Fields(DbModelBuilder modelBuilder)");
                stringGenerator.Braces(() => GenerateInitializer(model, stringGenerator));
            }
        }

        private void GenerateInitializer(Model model, IStringGenerator stringGenerator)
        {
            // suppressed mtm's
            foreach (var field in model.RelationFields.Active())
            {
                if (relationIds.Contains(field.AssociationId))
                {
                    continue;
                }

                stringGenerator.AppendLine("modelBuilder.Entity<" + model.Name + ">()");
                stringGenerator.PushIndent();
                relationIds.Add(field.AssociationId);
                GenerateManyToManyIgnore(field as ManyToManyField, stringGenerator);
                GenerateOneToManyInit(field as OneToManyField, stringGenerator);
                GenerateManyToOneInit(field as ManyToOneField, stringGenerator);
                stringGenerator.PopIndent();
            }

            foreach (var field in model.MappingFields.Where(NeedsInitializer))
            {
                stringGenerator.AppendLine("modelBuilder.Entity<" + model.Name + ">()");
                stringGenerator.PushIndent();
                stringGenerator.AppendLine(".Property(e => e." + field.Name + ")");
                stringGenerator.AppendLine(".HasPrecision(" + field.DbField.Precision + ", " + field.DbField.Scale + ");");
                stringGenerator.PopIndent();
            }
        }

        private void GenerateManyToOneInit(ManyToOneField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            var required = field.NearEndFields.ActiveAny(x => x.DbField.IsNullable) ? "Optional" : "Required";

            stringGenerator.AppendLine(".Has" + required + "(x => x." + field.Name + ")");
            var reverseField = field.FieldModel.RelationFields.Active().FirstOrDefault(x => x.AssociationId == field.AssociationId);
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".WithMany(" + reverse + ")");
            var objectString = objectStringService.CreateObjectString(field.NearEndFields.ActiveSelect(x => x.Name), "x", false);
            stringGenerator.AppendLine(".HasForeignKey(x => " + objectString + ");");
        }

        private void GenerateOneToManyInit(OneToManyField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            stringGenerator.AppendLine(".HasMany(x => x." + field.Name + ")");
            var reverseField = field.FieldModel.RelationFields.Active().FirstOrDefault(x => x.AssociationId == field.AssociationId);
            var required = field.FarEndFields.ActiveAny(x => x.DbField.IsNullable) ? "Optional" : "Required";
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".With" + required + "(" + reverse + ")");
            var objectString = objectStringService.CreateObjectString(field.FarEndFields.ActiveSelect(x => x.Name), "x", false);
            stringGenerator.AppendLine(".HasForeignKey(x => " + objectString + ");");
        }

        private void GenerateManyToManyIgnore(ManyToManyField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            stringGenerator.AppendLine(".Ignore(x => x." + field.Name + ");");
        }

        private void GenerateManyToManyInit(ManyToManyField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            relationIds.Add(field.MediatorMtoField.AssociationId);
            stringGenerator.AppendLine(".HasMany(x => x." + field.Name + ")");
            var reverseField = field.MediatorMtoField.FieldModel.RelationFields.Active().OfType<ManyToManyField>()
                .FirstOrDefault(x => x.MediatorMtoField.AssociationId == field.AssociationId);
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".WithMany(" + reverse + ")");
            stringGenerator.AppendLine(".Map(m => m.ToTable(\"" + field.MediatorModel.DbModel.Name + "\", \"" +
                                       field.MediatorModel.DbModel.Schema + "\")");
            stringGenerator.PushIndent();
            var leftKeys = field.FarEndFields.ActiveSelect(x => "\"" + x.DbField.Name + "\"");
            stringGenerator.AppendLine(".MapLeftKey(" + string.Join(", ", leftKeys) + ")");
            var rightKeys = field.MediatorMtoField.NearEndFields.ActiveSelect(x => "\"" + x.DbField.Name + "\"");
            stringGenerator.AppendLine(".MapRightKey(" + string.Join(", ", rightKeys) + "));");
            stringGenerator.PopIndent();
        }
    }
}
