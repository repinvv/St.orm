namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using StormGenerator.Generation.Generators;
    using StormGenerator.Generation.Generators.Linq2Db;
    using StormGenerator.Models;
    using StormGenerator.Settings;

    internal class Linq2DbContextCollection : IGeneratorCollection
    {
        private readonly Options options;

        public Linq2DbContextCollection(Options options)
        {
            this.options = options;
        }

        public IEnumerable<FileGenerator> GetFileGenerators(List<EntityModel> models, GenOptions options1)
        {
            yield return new StormContext(options.GenOptions, models);
        }
    }
}
