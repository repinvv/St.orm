namespace StormGenerator.Generation
{
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using StormGenerator.DatabaseReading;
    using StormGenerator.Generation.Models;
    using StormGenerator.Settings;

    internal class SchemaLoader
    {
        private readonly Options options;
        private readonly DbModelsReaderFactory factory;

        public SchemaLoader(Options options, DbModelsReaderFactory factory)
        {
            this.options = options;
            this.factory = factory;
        }

        public Schema LoadSchema(string schemaFile)
        {
            var schema = JsonConvert.DeserializeObject<Schema>(File.ReadAllText(schemaFile));
            if (schema.Tables != null && schema.Tables.Any() && !options.ForceRefreshDbInfo)
            {
                return schema;
            }

            schema.Tables = factory.GetReader().GetTables();
            var settings = new JsonSerializerSettings
                           {
                               Formatting = Formatting.Indented,
                               DefaultValueHandling = DefaultValueHandling.Ignore
                           };
            File.WriteAllText(schemaFile, JsonConvert.SerializeObject(schema, settings));
            return schema;
        }
    }
}
