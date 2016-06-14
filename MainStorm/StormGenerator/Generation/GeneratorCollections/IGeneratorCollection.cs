namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using StormGenerator.Models;
    using StormGenerator.Settings;

    internal interface IGeneratorCollection
    {
        IEnumerable<GeneratedFile> Generate(List<EntityModel> models, GenOptions options);
    }
}
