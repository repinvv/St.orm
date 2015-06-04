namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.Models.Config;
    using StormGenerator.ModelsCollection;

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
            var file = File.ReadAllText(options.SettingsFile);
            var stormConfig = new JavaScriptSerializer().Deserialize<StormConfig>(file);
            if (stormConfig.DbModels == null || !stormConfig.DbModels.Any() || options.ForceLoadDbInfo)
            {
                stormConfig.DbModels = dbmodelsCollector.GetModels(options);
                file = new JavaScriptSerializer().Serialize(stormConfig);
                File.WriteAllText(options.SettingsFile, file);
            }

            var models = modelsCollector.CollectModels(stormConfig);
            return null;
        }
    }
}
