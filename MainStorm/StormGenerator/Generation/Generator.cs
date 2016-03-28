namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Generator
    {
        private readonly SchemaLoader schemaLoader;

        public Generator(SchemaLoader schemaLoader)
        {
            this.schemaLoader = schemaLoader;
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
            
            return new List<GeneratedFile>();
        }
    }
}
