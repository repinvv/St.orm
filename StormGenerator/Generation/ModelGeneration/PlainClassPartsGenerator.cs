namespace StormGenerator.Generation.ModelGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.CommonGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class PlainClassPartsGenerator : IModelPartsGenerator
    {
        private readonly TypeNameService nameService;
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;

        public PlainClassPartsGenerator(TypeNameService nameService,
            UsingsGenerator usingsGenerator,
            FieldUtility fieldUtility)
        {
            this.nameService = nameService;
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Select(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.MappedClassUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public partial class " + model.Name);
        }

        public void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + nameService.GetTypeName(field.Type) + " " + field.Name + " { get;set; }");
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
