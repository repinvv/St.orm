namespace StormGenerator.Generation.ModelGeneration
{
    using StormGenerator.Common;
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class StructPartsGenerator : IModelPartsGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly FieldUtility fieldUtility;
        private readonly PlainClassPartsGenerator classPartsGenerator;
        private readonly OptionsService options;

        public StructPartsGenerator(UsingsGenerator usingsGenerator, 
            FieldUtility fieldUtility, 
            PlainClassPartsGenerator classPartsGenerator, 
            OptionsService options)
        {
            this.usingsGenerator = usingsGenerator;
            this.fieldUtility = fieldUtility;
            this.classPartsGenerator = classPartsGenerator;
            this.options = options;
        }

        public void GenerateUsings(Model model, IStringGenerator stringGenerator)
        {
            var usings = model.MappingFields.ActiveSelect(fieldUtility.GetUsing);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
        }

        public void GenerateDefinition(Model model, IStringGenerator stringGenerator)
        {
            var customInterface = string.IsNullOrWhiteSpace(options.Options.CustomInterfaceForEntities)
                                      ? string.Empty
                                      : " : " + options.Options.CustomInterfaceForEntities.Trim();
            stringGenerator.AppendLine("public partial struct " + model.Name + customInterface);
        }

        public void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator)
        {
            classPartsGenerator.GenerateMappingProperty(model, field, stringGenerator);
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        { }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        { }

        public void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator)
        { }
    }
}
