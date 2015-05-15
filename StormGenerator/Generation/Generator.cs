namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using StormGenerator.DbModelCollection;

    internal class Generator
    {
        private readonly DbModelsCollector modelsCollector;

        public Generator(DbModelsCollector modelsCollector)
        {
            this.modelsCollector = modelsCollector;
        }

        public List<GeneratedFile> Generate(string edmx, string config, string outputNamespace)
        {
            var models = modelsCollector.GetModels(edmx);
            return null;
        }
    }
}
