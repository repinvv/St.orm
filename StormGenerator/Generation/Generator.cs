namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using StormGenerator.Generation.ModelGeneration;
    using StormGenerator.Generation.RepositoryGeneration;
    using StormGenerator.Generation.StaticFilesGeneration;

    internal class Generator
    {
        private readonly ModelGenerator modelGenerator;
        private readonly StaticFilesGenerator staticFilesGenerator;
        private readonly RepositoryGenerator repositoryGenerator;
        private readonly ModelsCollectionService modelsCollectionService;
        private readonly ModelIterator modelIterator;

        public Generator(ModelGenerator modelGenerator,
            StaticFilesGenerator staticFilesGenerator,
            RepositoryGenerator repositoryGenerator,
            ModelsCollectionService modelsCollectionService,
            ModelIterator modelIterator)
        {
            this.modelGenerator = modelGenerator;
            this.staticFilesGenerator = staticFilesGenerator;
            this.repositoryGenerator = repositoryGenerator;
            this.modelsCollectionService = modelsCollectionService;
            this.modelIterator = modelIterator;
        }

        public List<GeneratedFile> Generate()
        {
            var models = modelsCollectionService.CollectModels();
            var output = new List<GeneratedFile>();
            modelIterator.ForAllModels(models, model => output.Add(modelGenerator.GenerateModel(model)));
            modelIterator.ForAllModels(models, model => output.Add(repositoryGenerator.GenerateRepository(model)));
            output.AddRange(staticFilesGenerator.GenerateStaticFiles(models));
            return output;
        }        
    }
}