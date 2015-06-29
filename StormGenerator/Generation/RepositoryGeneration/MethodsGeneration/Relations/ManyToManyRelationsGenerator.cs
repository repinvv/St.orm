namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToManyRelationsGenerator : IRelationsGenerator
    {
        public void GenerateSaveRelation(RelationField field, IStringGenerator stringGenerator)
        {
            var mtmField = field as ManyToManyField;
            if (field.IsList)
            {
                stringGenerator.AppendLine("foreach(var item in entity." + field.Name + ")");
                stringGenerator.Braces("saves.Save(" + CreateEntityString(mtmField, "item") + "));");
            }
            else
            {
                stringGenerator.AppendLine("if(entity." + field.Name + " != null)");
                stringGenerator.Braces("saves.Save(" + CreateEntityString(mtmField, "entity." + field.Name) + ");");
            }
        }

        private string CreateEntityString(ManyToManyField field, string accessor)
        {
            return "new " + field.MediatorModel.Name + " { " + field.FarEndFields[0].Name + " = " + field.FarEndFields[0].Name
                + ", " + field.MediatorMtoField.NearEndFields[0].Name + " = " + field.MediatorMtoField.FarEndFields[0].Name + "}";
        }

        public void GenerateUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        { }

        public void GenerateDeleteRelation(RelationField field, IStringGenerator stringGenerator)
        { }
    }
}
