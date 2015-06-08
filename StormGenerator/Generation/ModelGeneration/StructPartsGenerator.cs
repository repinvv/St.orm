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
        private readonly PlainClassPartsGenerator classPartsGenerator;

        public StructPartsGenerator(UsingsGenerator usingsGenerator, 
            FieldUtility fieldUtility, 
            PlainClassPartsGenerator classPartsGenerator)
        {
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.classPartsGenerator = classPartsGenerator;
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

        public void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            classPartsGenerator.GenerateMappingProperty(model, field, stringGenerator);
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        { }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        { }
    }
}
