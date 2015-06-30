namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
    using System.Security.Policy;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToManyRelationsGenerator : IRelationsGenerator
    {
        private readonly Generics generics;

        public ManyToManyRelationsGenerator(Generics generics)
        {
            this.generics = generics;
        }

        public void GenerateSaveRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var mtmField = field as ManyToManyField;
            stringGenerator.AppendLine("if(entity." + field.Name + " != null)");
            stringGenerator.Braces(() =>
            {
                if (field.IsList)
                {
                    stringGenerator.AppendLine("foreach(var item in entity." + field.Name + ")");
                    stringGenerator.Braces(() => GenerateSave(mtmField, "item", stringGenerator));
                }
                else
                {
                    GenerateSave(mtmField, "entity." + field.Name, stringGenerator);
                }
            });
            stringGenerator.AppendLine();
        }

        public void GenerateUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        {
            if (field.IsList)
            {
                GenerateListUpdateRelation(field, stringGenerator);
            }
            else
            {
                GenerateSingleUpdateRelation(field, stringGenerator);
            }
        }

        public void GenerateDeleteRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var mtmField = field as ManyToManyField;
            stringGenerator.AppendLine("if(entity." + field.Name + " != null)");
            stringGenerator.Braces(() =>
            {
                if (field.IsList)
                {
                    stringGenerator.AppendLine("foreach(var item in entity." + field.Name + ")");
                    stringGenerator.Braces(() => GenerateDelete(mtmField, "item", stringGenerator));
                }
                else
                {
                    GenerateDelete(mtmField, "entity." + field.Name, stringGenerator);
                }
            });
            stringGenerator.AppendLine();
        }

        private void GenerateListUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var mtmField = field as ManyToManyField;
            stringGenerator.AppendLine("var items = entity." + field.Name + " ?? new List<" + field.FieldModel.Name + ">();");
            stringGenerator.AppendLine("var entitySet = new HashSet<" + field.FieldModel.Name + ">(items);");
            stringGenerator.AppendLine("foreach (var item in existing." + field.Name + ".Where(x => !entitySet.Contains(x)))");
            stringGenerator.Braces(() => GenerateDelete(mtmField, "item", stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("var existingSet = new HashSet<" + field.FieldModel.Name + ">(existing." + field.Name + ");");
            stringGenerator.AppendLine("foreach (var item in items.Where(x => !existingSet.Contains(x)))");
            stringGenerator.Braces(() => GenerateSave(mtmField, "item", stringGenerator));
        }

        private void GenerateSingleUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var mtmField = field as ManyToManyField;
            stringGenerator.AppendLine("if(entity." + field.Name  + " != existing." + field.Name + ")");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("if(existing." + field.Name + " != null)");
                stringGenerator.Braces(() => GenerateDelete(mtmField, "existing." + field.Name, stringGenerator));
                stringGenerator.AppendLine("if(entity." + field.Name + " != null)");
                stringGenerator.Braces(() => GenerateSave(mtmField, "entity." + field.Name, stringGenerator));
            });
        }

        private void GenerateSave(ManyToManyField mtmField, string accessor, IStringGenerator stringGenerator)
        {
            GenerateCreateMediator(mtmField, accessor, stringGenerator);
            stringGenerator.AppendLine("saves.Save" + generics.Line(mtmField.MediatorModel) + "(mediator);");
        }

        private void GenerateDelete(ManyToManyField mtmField, string accessor, IStringGenerator stringGenerator)
        {
            GenerateCreateMediator(mtmField, accessor, stringGenerator);
            stringGenerator.AppendLine("saves.Delete" + generics.Line(mtmField.MediatorModel) + "(mediator);");
        }

        private void GenerateCreateMediator(ManyToManyField mtmField, string accessor, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var mediator = new " + mtmField.MediatorModel.Name);
            stringGenerator.Braces(() => GenerateMediatorContent(mtmField, accessor, stringGenerator), true);
        }

        private void GenerateMediatorContent(ManyToManyField field, string accessor, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(field.FarEndFields[0].Name + " = entity." + field.FarEndFields[0].Name + ",");
            stringGenerator.AppendLine(field.MediatorMtoField.NearEndFields[0].Name + " = " + accessor +
                                       "." + field.MediatorMtoField.FarEndFields[0].Name);
        }
    }
}
