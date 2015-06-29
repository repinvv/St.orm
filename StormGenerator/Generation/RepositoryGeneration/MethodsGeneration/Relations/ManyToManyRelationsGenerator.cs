namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
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

        private void GenerateSave(ManyToManyField mtmField, string accessor, IStringGenerator stringGenerator)
        {
            GenerateCreateMediator(mtmField, accessor, stringGenerator);
            stringGenerator.AppendLine("saves.Save" + generics.Line(mtmField.MediatorModel) + "(mediator);");
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

        public void GenerateUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        { }

        public void GenerateDeleteRelation(RelationField field, IStringGenerator stringGenerator)
        { }
    }
}
