namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using StormGenerator.Generation.Generators;
    using StormGenerator.Models;
    using StormGenerator.Settings;

    internal interface IGeneratorCollection
    {
        IEnumerable<FileGenerator> GetFileGenerators(List<EntityModel> models, GenOptions options);
    }
}
