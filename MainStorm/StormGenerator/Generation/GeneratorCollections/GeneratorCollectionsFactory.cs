namespace StormGenerator.Generation.GeneratorCollections
{
    using System.Collections.Generic;
    using StormGenerator.Settings;

    internal class GeneratorCollectionsFactory
    {
        private readonly ModelsCollection modelsCollection;
        private readonly Linq2DbContextCollection linq2DbContextCollection;
        private GenOptions options;

        public GeneratorCollectionsFactory(GenerationOptionsService generationOptionsService,
            ModelsCollection modelsCollection,
            Linq2DbContextCollection linq2DbContextCollection)
        {
            options = generationOptionsService.GenOptions;
            this.modelsCollection = modelsCollection;
            this.linq2DbContextCollection = linq2DbContextCollection;
        }

        public IEnumerable<IGeneratorCollection> GetGeneratorCollections()
        {
            yield return modelsCollection;
            // yield return linq2DbContextCollection;
        } 
    }
}
