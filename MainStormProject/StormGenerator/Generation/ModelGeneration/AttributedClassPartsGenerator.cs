namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class AttributedClassPartsGenerator : IModelPartsGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly PlainClassPartsGenerator plainClassPartsGenerator;
        private readonly IdentityFinder identityFinder;
        private readonly Type[] integerTypes = { typeof(int), typeof(short), typeof(long) };

        public AttributedClassPartsGenerator(
            UsingsGenerator usingsGenerator,
            FieldUtility fieldUtility,
            PlainClassPartsGenerator plainClassPartsGenerator,
            IdentityFinder identityFinder)
        {
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.plainClassPartsGenerator = plainClassPartsGenerator;
            this.identityFinder = identityFinder;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.ActiveSelect(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.ModelClassUsings)
                              .Concat(GenerationConstants.ModelGeneration.EfAttributesUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("[Table(\"" + model.DbModel.Name + "\", Schema = \"" + model.DbModel.Schema + "\")]");
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
                else if (integerTypes.Contains(field.Type) && identityFinder.HasId(model))
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

                if (field.DbField.Length > 0)
                {
                    stringGenerator.AppendLine("[MaxLength(" + field.DbField.Length + ")]");
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

        public void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator)
        {
            plainClassPartsGenerator.GenerateCloneableMembers(model, stringGenerator);
        }
    }
}
