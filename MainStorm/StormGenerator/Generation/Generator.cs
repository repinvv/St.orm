namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.ModelsCreation;

    internal class Generator
    {
        private readonly SchemaLoader schemaLoader;
        private readonly ModelsCreation modelsFromConfigsCreation;

        public Generator(SchemaLoader schemaLoader, ModelsCreation modelsFromConfigsCreation)
        {
            this.schemaLoader = schemaLoader;
            this.modelsFromConfigsCreation = modelsFromConfigsCreation;
        }

        public List<GeneratedFile> Generate(string schemaFile)
        {
            var schema = schemaLoader.LoadSchema(schemaFile);
            if (!schema.Configs.Any())
            {
                var file = new GeneratedFile
                {
                    Name = "NoModels.cs",
                    Content = "// No models configured"
                };
                return new List<GeneratedFile> { file };
            }

            var models = modelsFromConfigsCreation.CreateModelsFromSchema(schema);
            return new List<GeneratedFile>();
        }
    }
}
