namespace StormGenerator.Generation
{
    using System.Collections.Generic;

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
            return null;
        }
    }
}
