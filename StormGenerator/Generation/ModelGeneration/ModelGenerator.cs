namespace StormGenerator.Generation.ModelGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ModelGenerator
    {
        private readonly IStringGenerator stringGenerator;
        private readonly UsingsGenerator usingsGenerator;

        public ModelGenerator(IStringGenerator stringGenerator, UsingsGenerator usingsGenerator)
        {
            this.stringGenerator = stringGenerator;
            this.usingsGenerator = usingsGenerator;
        }

        public GeneratedFile GenerateModel(Model model, string outputNamespace)
        {
            return new GeneratedFile
                   {
                       Name = model.Name,
                       Content = GenerateModelContent(model, outputNamespace)
                   };
        }

        private string GenerateModelContent(Model model, string outputNamespace)
        {
            stringGenerator.AppendLine("namespace " + outputNamespace);
            stringGenerator.Braces(() => GenerateModelDefinition(model));
            return stringGenerator.ToString();
        }

        private void GenerateModelDefinition(Model model)
        {
            var usings = model.MappingFields.Select(GetUsing).ToList();
            usings.AddRange(GenerationConstants.ModelGeneration.Usings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
            stringGenerator.AppendLine();
            if (!model.IsStruct)
            {
                stringGenerator.AppendLine("[Table(\"" + model.DbModel.Name + "\")]");
            }

            var type = model.IsStruct ? "struct" : "class";
            stringGenerator.AppendLine("public partial " + type + " " + model.Name);
            stringGenerator.Braces(() => GenerateContents(model));
        }

        private void GenerateContents(Model model)
        {
            GenerateClonedFrom(model);
            bool first = true;
            foreach (var mappingField in model.MappingFields)
            {
                if (!first)
                {
                    stringGenerator.AppendLine();
                }

                GenerateProperty(mappingField, !model.IsStruct);
                first = false;
            }
        }

        private void GenerateProperty(MappingField mappingField, bool generateAttributes)
        {
            if (generateAttributes)
            {
                if (mappingField.DbField.IsPrimaryKey)
                {
                    stringGenerator.AppendLine("[Key]");
                    if (mappingField.DbField.IsIdentity)
                    {
                        stringGenerator.AppendLine("[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                }

                if (mappingField.Type == typeof(string))
                {
                    if (!mappingField.DbField.IsNullable)
                    {
                        stringGenerator.AppendLine("[Required]");
                    }

                    if (mappingField.DbField.StringLength > 0)
                    {
                        stringGenerator.AppendLine("[MaxLength(" + mappingField.DbField.StringLength + ")]");
                    }
                }

                stringGenerator.AppendLine("[Column(\"" + mappingField.DbField.Name + "\")]");
            }

            stringGenerator.AppendLine("public " + mappingField.Type.GetTypeName() + " " + mappingField.Name + " { get;set; }");
        }

        private string GetUsing(MappingField arg)
        {
            return arg.Type.Namespace;
        }

        private void GenerateClonedFrom(Model model)
        {
            if (model.IsStruct)
            {
                return;
            }

            stringGenerator.AppendLine("private " + model.Name + " clonedFrom;");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "(" + model.Name + " clonedFrom)");
            stringGenerator.Braces(() => stringGenerator.AppendLine("this.clonedFrom = clonedFrom;"));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "() { }");
            stringGenerator.AppendLine();
        }
    }
}
