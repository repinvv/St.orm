namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal class OneToManyRelationsGenerator : IRelationsGenerator
    {
        public void GenerateSaveRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var otmField = field as OneToManyField;
            GeneratePreAssignments(otmField, stringGenerator);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("SaveService.Save<" + field.FieldModel.Name + ", " + field.FieldModel.Parent.Name + ">(entity."
                                       + field.Name + ", saves);");
            stringGenerator.AppendLine();
        }

        public void GenerateUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var otmField = field as OneToManyField;
            GeneratePreAssignments(otmField, stringGenerator);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("SaveService.Update<" + field.FieldModel.Name + ", " + field.FieldModel.Parent.Name + ">(entity."
                                       + field.Name + ", existing." + field.Name + ", saves);");
        }

        public void GenerateDeleteRelation(RelationField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("SaveService.Delete<" + field.FieldModel.Name + ", " + field.FieldModel.Parent.Name + ">(entity."
                                       + field.Name + ", saves);");
        }

        private void GeneratePreAssignments(RelationField field, IStringGenerator stringGenerator)
        {
            if (field.IsList)
            {
                GenerateArrayPreAssignments(field, stringGenerator);
            }
            else
            {
                GenerateSinglePreAssignments(field, stringGenerator);
            }
        }

        private void GenerateArrayPreAssignments(RelationField field, IStringGenerator stringGenerator)
        {
            var accessor = "entity." + field.Name;
            stringGenerator.AppendLine("if (" + accessor + " != null)");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("foreach (var field in " + accessor + ")");
                stringGenerator.Braces(() => GenerateFieldsUpdate(field, stringGenerator, "field"));
            });
        }

        private void GenerateSinglePreAssignments(RelationField field, IStringGenerator stringGenerator)
        {
            var accessor = "entity." + field.Name;
            stringGenerator.AppendLine("if (" + accessor + " != null)");
            stringGenerator.Braces(() => GenerateFieldsUpdate(field, stringGenerator, accessor));
            stringGenerator.AppendLine();
        }

        private void GenerateFieldsUpdate(RelationField field, IStringGenerator stringGenerator, string accessor)
        {
            for (int i = 0; i < field.NearEndFields.Count; i++)
            {
                var sourceField = field.NearEndFields[i];
                var targetField = field.FarEndFields[i];
                stringGenerator.AppendLine(accessor + "." + targetField.Name + " = entity." + sourceField.Name + ";");
            }
        }
    }
}
