namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using StormGenerator.Common;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.Infrastructure;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Pregen;
    using StormGenerator.ModelsCollection;

    internal class ModelsCollectionService
    {
        private readonly DbModelsCollector dbmodelsCollector;
        private readonly ModelsCollector modelsCollector;
        private readonly NameNormalizer nameNormalizer;
        private readonly OptionsService options;

        public ModelsCollectionService(DbModelsCollector dbmodelsCollector,
            ModelsCollector modelsCollector,
            NameNormalizer nameNormalizer, 
            OptionsService options)
        {
            this.dbmodelsCollector = dbmodelsCollector;
            this.modelsCollector = modelsCollector;
            this.nameNormalizer = nameNormalizer;
            this.options = options;
        }

        public List<Model> CollectModels()
        {
            var file = File.ReadAllText(options.Options.SettingsFile);
            var stormConfig = new JavaScriptSerializer().Deserialize<StormConfig>(file);
            if (stormConfig.DbModels == null || !stormConfig.DbModels.Any() || options.Options.ForceLoadDbInfo)
            {
                stormConfig.DbModels = dbmodelsCollector.GetModels();
                file = new JavaScriptSerializer().Serialize(stormConfig);
                File.WriteAllText(options.Options.SettingsFile, file);
            }

            var models = modelsCollector.CollectModels(stormConfig);
            var names = nameNormalizer.NormalizeNames(models.Select(x => x.Name)
                                                            .ToList());
            for (int index = 0; index < models.Count; index++)
            {
                models[index].Name = names[index];
            }

            return models;
        }
    }
}
