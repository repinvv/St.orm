namespace StormGenerator.Generation.ContextGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Generation.CommonGeneration;
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

        public void GenerateInitializers(List<Model> models, IStringGenerator stringGenerator)
        {
            foreach (var model in models.Where(x => x.RelationFields.Any()))
            {
                stringGenerator.AppendLine();
                stringGenerator.AppendLine("protected virtual void Initialize" + model.Name + "Relations(DbModelBuilder modelBuilder)");
                stringGenerator.Braces(() => GenerateInitializer(model, stringGenerator));
            }
        }

        private void GenerateInitializer(Model model, IStringGenerator stringGenerator)
        {
            foreach (var field in model.RelationFields)
            {
                if (relationIds.Contains(field.AssociationId))
                {
                    continue;
                }

                stringGenerator.AppendLine("modelBuilder.Entity<" + model.Name + ">()");
                stringGenerator.PushIndent();
                relationIds.Add(field.AssociationId);
                GenerateManyToManyInit(model, field as ManyToManyField, stringGenerator);
                GenerateOneToManyInit(model, field as OneToManyField, stringGenerator);
                GenerateManyToOneInit(model, field as ManyToOneField, stringGenerator);
                stringGenerator.PopIndent();
            }
        }

        private void GenerateManyToOneInit(Model model, ManyToOneField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            var required = field.NearEndFields.Any(x => x.DbField.IsNullable) ? "Optional" : "Required";

            stringGenerator.AppendLine(".Has" + required + "(x => x." + field.Name + ")");
            var reverseField = field.FieldModel.RelationFields.FirstOrDefault(x => x.AssociationId == field.AssociationId);
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".WithMany(" + reverse + ")");
            var objectString = objectStringService.CreateObjectString(field.NearEndFields.Select(x => x.Name).ToArray(), "x");
            stringGenerator.AppendLine(".HasForeignKey(x => " + objectString + ");");
        }

        private void GenerateOneToManyInit(Model model, OneToManyField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            stringGenerator.AppendLine(".HasMany(x => x." + field.Name + ")");
            var reverseField = field.FieldModel.RelationFields.FirstOrDefault(x => x.AssociationId == field.AssociationId);
            var required = field.FarEndFields.Any(x => x.DbField.IsNullable) ? "Optional" : "Required";
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".With" + required + "(" + reverse + ")");
            var objectString = objectStringService.CreateObjectString(field.FarEndFields.Select(x => x.Name).ToArray(), "x");
            stringGenerator.AppendLine(".HasForeignKey(x => " + objectString + ");");
        }

        private void GenerateManyToManyInit(Model model, ManyToManyField field, IStringGenerator stringGenerator)
        {
            if (field == null)
            {
                return;
            }

            relationIds.Add(field.MediatorMtoField.AssociationId);
            stringGenerator.AppendLine(".HasMany(x => x." + field.Name + ")");
            var reverseField = field.MediatorMtoField.FieldModel.RelationFields.OfType<ManyToManyField>()
                .FirstOrDefault(x => x.MediatorMtoField.AssociationId == field.AssociationId);
            var reverse = reverseField == null ? string.Empty : ("x => x." + reverseField.Name);
            stringGenerator.AppendLine(".WithMany(" + reverse + ")");
            stringGenerator.AppendLine(".Map(m => m.ToTable(\"" + field.MediatorModel.DbModel.Id + "\")");
            stringGenerator.PushIndent();
            var leftKeys = field.FarEndFields.Select(x => "\"" + x.DbField.Name + "\"");
            stringGenerator.AppendLine(".MapLeftKey(" + string.Join(", ", leftKeys) + ")");
            var rightKeys = field.MediatorMtoField.NearEndFields.Select(x => "\"" + x.DbField.Name + "\"");
            stringGenerator.AppendLine(".MapRightKey(" + string.Join(", ", rightKeys) + "));");
            stringGenerator.PopIndent();
        }
    }
}
