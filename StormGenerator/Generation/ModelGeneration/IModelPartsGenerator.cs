namespace StormGenerator.Generation.ModelGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IModelPartsGenerator
    {
        void GenerateUsings(Model model, IStringGenerator stringGenerator);

        void GenerateDefinition(Model model, IStringGenerator stringGenerator);

        void GenerateMappingProperty(MappingField field, IStringGenerator stringGenerator);

        void GeneratePrivateFields(Model model, IStringGenerator stringGenerator);

        void GenerateConstructors(Model model, IStringGenerator stringGenerator);
    }
}
