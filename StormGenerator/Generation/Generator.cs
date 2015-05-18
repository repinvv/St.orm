namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using StormGenerator.DbModelCollection;
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

        public List<GeneratedFile> Generate(string edmx, string config, string outputNamespace)
        {
            var dbmodels = dbmodelsCollector.GetModels(edmx);
            var models = modelsCollector.CollectModelsWithSettings(dbmodels, config);
            return null;
        }
    }
}
