namespace StormGenerator.Generation.ModelGeneration.ModelPartsGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IModelPartsGenerator
    {
        IEnumerable<string> GetUsings(Model model);

        string GetDefinition(Model model);

        void GenerateConstructors(Model model, IStringGenerator stringGenerator);

        void GeneratePrivateFields(Model model, IStringGenerator stringGenerator);

        void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator);
    }
}
