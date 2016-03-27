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
            var file = new GeneratedFile
                       {
                           Name = "StormContext.cs",
                           Content = "//Context content Will Be here someday"
                       };
            return new List<GeneratedFile> { file };
        }
    }
}
