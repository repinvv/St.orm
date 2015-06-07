namespace StormGenerator.Generation.ModelGeneration
{
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.CommonGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class StructPartsGenerator : IModelPartsGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly TypeNameService nameService;

        public StructPartsGenerator(UsingsGenerator usingsGenerator, FieldUtility fieldUtility, TypeNameService nameService)
        {
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.nameService = nameService;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.Select(fieldUtility.GetUsing);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public partial struct " + model.Name);
        }

        public void GenerateMappingProperty(MappingField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + nameService.GetTypeName(field.Type) + " " + field.Name + " { get;set; }");
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        { }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        { }
    }
}
