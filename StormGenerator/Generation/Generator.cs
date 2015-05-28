namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.Models.Config;

    internal class Generator
    {
        private readonly DbModelsCollector dbmodelsCollector;

        public Generator(DbModelsCollector dbmodelsCollector)
        {
            this.dbmodelsCollector = dbmodelsCollector;
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
            
            return null;
        }
    }
}
