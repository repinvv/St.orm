namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using StormGenerator.Generation.Generators;
    using StormGenerator.Generation.Generators.MsSqlCiServices;
    using StormGenerator.Models;
    using StormGenerator.Settings;

    internal class MsSqlCiServicesCollection : IGeneratorCollection
    {
        public IEnumerable<FileGenerator> GetFileGenerators(List<EntityModel> models, GenOptions options)
        {
            yield return new MsSqlCi(options, models);
            yield return new MsSqlCiServiceInterface(options);
            yield return new CiHelper(options);
            foreach (var entityModel in models)
            {
                yield return new MsSqlCiService(options, entityModel);
            }
        }
    }
}
