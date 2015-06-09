namespace StormGenerator.Generation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using StormGenerator.Common;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.Generation.ModelGeneration;
    using StormGenerator.Generation.RepositoryGeneration;
    using StormGenerator.Generation.StaticFilesGeneration;
    using StormGenerator.Models.Config;
    using StormGenerator.ModelsCollection;

    internal class Generator
    {
        private readonly DbModelsCollector dbmodelsCollector;
        private readonly ModelsCollector modelsCollector;
        private readonly ModelGenerator modelGenerator;
        private readonly StaticFilesGenerator staticFilesGenerator;
        private readonly NameNormalizer nameNormalizer;
        private readonly RepositoryGenerator repositoryGenerator;

        public Generator(DbModelsCollector dbmodelsCollector,
            ModelsCollector modelsCollector, 
            ModelGenerator modelGenerator,
            StaticFilesGenerator staticFilesGenerator,
            NameNormalizer nameNormalizer,
            RepositoryGenerator repositoryGenerator)
        {
            this.dbmodelsCollector = dbmodelsCollector;
            this.modelsCollector = modelsCollector;
            this.modelGenerator = modelGenerator;
            this.staticFilesGenerator = staticFilesGenerator;
            this.nameNormalizer = nameNormalizer;
            this.repositoryGenerator = repositoryGenerator;
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
            var names = nameNormalizer.NormalizeNames(models.Select(x => x.Name).ToList());
            for (int index = 0; index < models.Count; index++)
            {
                models[index].Name = names[index];
            }

            var output = new List<GeneratedFile>();

            output.AddRange(models.Select(x => modelGenerator.GenerateModel(x, options)));
            output.AddRange(models.Select(x => repositoryGenerator.GenerateRepository(x, options)));
            output.AddRange(staticFilesGenerator.GenerateStaticFiles(models, options));
            return output;
        }
    }
}
