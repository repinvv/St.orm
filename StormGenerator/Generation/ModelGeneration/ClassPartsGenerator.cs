namespace StormGenerator.Generation.ModelGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ClassPartsGenerator : IModelPartsGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly StructPartsGenerator structPartsGenerator;

        public ClassPartsGenerator(UsingsGenerator usingsGenerator, FieldUtility fieldUtility, StructPartsGenerator structPartsGenerator)
        {
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.structPartsGenerator = structPartsGenerator;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Select(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.MappedClassUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("[Table(\"" + model.DbModel.Id + "\")]");
            stringGenerator.AppendLine("public partial class " + model.Name);
        }

        public void GenerateMappingProperty(MappingField field, IStringGenerator stringGenerator)
        {
            if (field.DbField.IsPrimaryKey)
            {
                stringGenerator.AppendLine("[Key]");
                if (field.DbField.IsIdentity)
                {
                    stringGenerator.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                }
            }

            if (field.Type == typeof(string))
            {
                if (!field.DbField.IsNullable)
                {
                    stringGenerator.AppendLine("[Required]");
                }

                if (field.DbField.StringLength > 0)
                {
                    stringGenerator.AppendLine("[MaxLength(" + field.DbField.StringLength + ")]");
                }
            }

            stringGenerator.AppendLine("[Column(\"" + field.DbField.Name + "\", Order = " + field.DbField.Index + ")]");
            structPartsGenerator.GenerateMappingProperty(field, stringGenerator);
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private " + model.Name + " clonedFrom;");
        }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + "(" + model.Name + " clonedFrom)");
            stringGenerator.Braces(() => stringGenerator.AppendLine("this.clonedFrom = clonedFrom;"));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "() { }");
        }
    }
}
