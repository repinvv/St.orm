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

        public Generator(DbModelsCollector dbmodelsCollector,
            ModelsCollector modelsCollector, 
            ModelGenerator modelGenerator,
            ContextGenerator contextGenerator)
        {
            this.dbmodelsCollector = dbmodelsCollector;
            this.modelsCollector = modelsCollector;
            this.modelGenerator = modelGenerator;
            this.contextGenerator = contextGenerator;
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
            var output = new List<GeneratedFile>();

            output.AddRange(models.Select(x => modelGenerator.GenerateModel(x, options.OutputNamespace)));
            output.Add(contextGenerator.GenerateContext(models, options.ContextName, options.OutputNamespace));

            output.ForEach(x => x.Name = x.Name + ".cs");
            output.ForEach(x => x.Content = GenerationConstants.GenerationMark + Environment.NewLine + x.Content);
            return output;
        }
    }
}
