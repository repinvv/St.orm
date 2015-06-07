namespace StormGenerator.Generation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using StormGenerator.Common;
    using StormGenerator.DbModelsCollection;
    using StormGenerator.Generation.ContextGeneration;
    using StormGenerator.Generation.ModelGeneration;
    using StormGenerator.Models.Config;
    using StormGenerator.ModelsCollection;

    internal class Generator
    {
        private readonly DbModelsCollector dbmodelsCollector;
        private readonly ModelsCollector modelsCollector;
        private readonly ModelGenerator modelGenerator;
        private readonly ContextGenerator contextGenerator;
        private readonly NameNormalizer nameNormalizer;

        public Generator(DbModelsCollector dbmodelsCollector,
            ModelsCollector modelsCollector, 
            ModelGenerator modelGenerator,
            ContextGenerator contextGenerator,
            NameNormalizer nameNormalizer)
        {
            this.dbmodelsCollector = dbmodelsCollector;
            this.modelsCollector = modelsCollector;
            this.modelGenerator = modelGenerator;
            this.contextGenerator = contextGenerator;
            this.nameNormalizer = nameNormalizer;
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

            output.AddRange(models.Select(x => modelGenerator.GenerateModel(x, options.OutputNamespace)));
            output.Add(contextGenerator.GenerateContext(models, options.ContextName, options.OutputNamespace));

            output.ForEach(x => x.Name = x.Name + ".cs");
            output.ForEach(x => x.Content = GenerationConstants.GenerationMark + Environment.NewLine + x.Content);
            return output;
        }
    }
}
