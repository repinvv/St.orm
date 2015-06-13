namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using StormGenerator.Common;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.Models.Config;
    using StormGenerator.Models.Pregen;
    using StormGenerator.ModelsCollection;

    internal class ModelsCollectionService
    {
        private readonly DbModelsCollector dbmodelsCollector;
        private readonly ModelsCollector modelsCollector;
        private readonly NameNormalizer nameNormalizer;

        public ModelsCollectionService(DbModelsCollector dbmodelsCollector,
            ModelsCollector modelsCollector,
            NameNormalizer nameNormalizer)
        {
            this.dbmodelsCollector = dbmodelsCollector;
            this.modelsCollector = modelsCollector;
            this.nameNormalizer = nameNormalizer;
        }

        public List<Model> CollectModels(Options options)
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
