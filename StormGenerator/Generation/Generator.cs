namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.ModelsCollector;

    internal class Generator
    {
        private readonly DbModelsCollector dbmodelsCollector;
        private readonly ModelsCollector modelsCollector;

        public Generator(DbModelsCollector dbmodelsCollector, ModelsCollector modelsCollector)
        {
            this.dbmodelsCollector = dbmodelsCollector;
            this.modelsCollector = modelsCollector;
        }

        public List<GeneratedFile> Generate(Options options)
        {
            var dbmodels = dbmodelsCollector.GetModels(options);
            ////var models = modelsCollector.CollectModelsWithSettings(dbmodels, options);
            return null;
        }
    }
}
