namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Generation.GeneratorCollections;
    using StormGenerator.ModelsCreation;
    using StormGenerator.Settings;

    internal class Generator
    {
        private readonly SchemaLoader schemaLoader;
        private readonly ModelsCreation modelsFromConfigsCreation;
        private readonly GeneratorCollectionsFactory collectionsFactory;
        private readonly GenOptions options;

        public Generator(SchemaLoader schemaLoader, 
            ModelsCreation modelsFromConfigsCreation,
            GeneratorCollectionsFactory collectionsFactory,
            GenerationOptionsService generationOptionsService)
        {
            this.options = generationOptionsService.GenOptions;
            this.schemaLoader = schemaLoader;
            this.modelsFromConfigsCreation = modelsFromConfigsCreation;
            this.collectionsFactory = collectionsFactory;
        }

        public List<GeneratedFile> Generate(string schemaFile)
        {
            var schema = schemaLoader.LoadSchema(schemaFile);
            if (!schema.Configs.Any())
            {
                var file = new GeneratedFile
                {
                    Name = "NoModels.cs",
                    Content = "// No models configured"
                };
                return new List<GeneratedFile> { file };
            }

            var models = modelsFromConfigsCreation.CreateModelsFromSchema(schema).Where(x=>x.Model.IsEnabled).ToList();
            var collection = collectionsFactory.GetGeneratorCollections();

            return collection
                .SelectMany(x => x.GetFileGenerators(models, options))
                .Select(x => x.GetFile())
                .ToList();
        }
    }
}
