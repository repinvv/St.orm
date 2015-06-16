namespace StormGenerator.Generation.ModelGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IModelPartsGenerator
    {
        void GenerateUsings(Model model, IStringGenerator stringGenerator);

        void GenerateDefinition(Model model, IStringGenerator stringGenerator);

        void GenerateMappingProperty(Model model, MappingField field, IStringGenerator stringGenerator);

        void GeneratePrivateFields(Model model, Model parent, IStringGenerator stringGenerator);

        void GenerateConstructors(Model model, Model parent, IStringGenerator stringGenerator);

        void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator);
    }
}
