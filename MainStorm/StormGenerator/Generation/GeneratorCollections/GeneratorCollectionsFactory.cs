namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using StormGenerator.Settings;

    internal class GeneratorCollectionsFactory
    {
        private GenOptions options;

        public GeneratorCollectionsFactory(GenerationOptionsService generationOptionsService)
        {
            options = generationOptionsService.GenOptions;
        }

        public IEnumerable<IGeneratorCollection> GetGeneratorCollections()
        {
            yield return new ModelsCollection();
            if (options.DbProvider == DbProvider.MsSql)
            {
                yield return new MsSqlCiServicesCollection();
            }
            if (options.Linq2EntitiesProvider == Linq2EntitiesProvider.linq2db)
            {
                // yield return linq2DbContextCollection;
            }
        } 
    }
}
