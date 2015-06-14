namespace StormGenerator.Generation.ModelGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class PlainClassPartsGenerator : IModelPartsGenerator
    {
        private readonly TypeService service;
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;

        public PlainClassPartsGenerator(TypeService service,
            UsingsGenerator usingsGenerator,
            FieldUtility fieldUtility)
        {
            this.service = service;
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Select(fieldUtility.GetUsing)
                              .Concat(GenerationConstants.ModelGeneration.ModelClassUsings);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public partial class " + model.Name);
        }

        public void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + service.GetTypeName(field.Type) + " " + field.Name + " { get;set; }");
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private readonly ILoadService loadService;");
            stringGenerator.AppendLine("private readonly " + model.Name + " clonedFrom;");
        }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + "(" + model.Name + " clonedFrom)");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("this.clonedFrom = clonedFrom;");
                stringGenerator.AppendLine("this.loadService = clonedFrom.GetLoadService();");
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "(ILoadService loadService)");
            stringGenerator.Braces(() => stringGenerator.AppendLine("this.loadService = loadService;"));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public " + model.Name + "()");
            stringGenerator.Braces(() =>
            {
                foreach (var field in model.RelationFields.Where(x => x.IsList))
                {
                    stringGenerator.AppendLine(field.Name + " = new HashSet<" + field.FieldModel.Name + ">();");
                }
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public ILoadService GetLoadService()");
            stringGenerator.Braces(() => stringGenerator.AppendLine("return loadService;"));
        }
    }
}
