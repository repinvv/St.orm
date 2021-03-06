﻿namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Generation.Generators;
    using StormGenerator.Generation.Generators.Models;
    using StormGenerator.Models;
    using StormGenerator.Settings;

    internal class ModelsCollection : IGeneratorCollection
    {
        public IEnumerable<FileGenerator> GetFileGenerators(List<EntityModel> models, GenOptions options)
        {
            return models.Select(x => new MainModel(options, x));
        }
    }
}
