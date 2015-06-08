namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class AttributedClassPartsGenerator : IModelPartsGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly PlainClassPartsGenerator plainClassPartsGenerator;
        private readonly Type[] integerTypes = { typeof(int), typeof(short), typeof(long) };

        public AttributedClassPartsGenerator(UsingsGenerator usingsGenerator, 
            FieldUtility fieldUtility, 
            PlainClassPartsGenerator plainClassPartsGenerator)
        {
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.plainClassPartsGenerator = plainClassPartsGenerator;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Select(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.MappedClassUsings)
                              .Concat(GenerationConstants.ModelGeneration.EfAttributesUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("[Table(\"" + model.DbModel.Id + "\")]");
            plainClassPartsGenerator.GenerateDefinition(model, stringGenerator);
        }

        public void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            if (field.DbField.IsPrimaryKey)
            {
                stringGenerator.AppendLine("[Key]");
                if (field.DbField.IsIdentity)
                {
                    stringGenerator.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                }
                else if (integerTypes.Contains(field.Type) && model.MappingFields.Count(x => x.DbField.IsPrimaryKey) == 1)
                {
                    stringGenerator.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.None)]");
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
            plainClassPartsGenerator.GenerateMappingProperty(model, field, stringGenerator);
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        {
            plainClassPartsGenerator.GeneratePrivateFields(model, stringGenerator);
        }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        {
            plainClassPartsGenerator.GenerateConstructors(model, stringGenerator);
        }
    }
}
