namespace StormGenerator.Generation
{
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using StormGenerator.AutomaticPopulation;
    using StormGenerator.DatabaseReading;
    using StormGenerator.Models;
    using StormGenerator.Settings;

    internal class SchemaLoader
    {
        private readonly Options options;
        private readonly DbModelsReaderFactory factory;
        private readonly AutomaticModelConfigsPopulation autoPopulation;

        public SchemaLoader(Options options,
            DbModelsReaderFactory factory,
            AutomaticModelConfigsPopulation autoPopulation)
        {
            this.options = options;
            this.factory = factory;
            this.autoPopulation = autoPopulation;
        }

        public Schema LoadSchema(string schemaFile)
        {
            var schema = JsonConvert.DeserializeObject<Schema>(File.ReadAllText(schemaFile)) ?? new Schema();
            var save = false;
            if (schema.Tables?.Count < 1 || options.ForceRefreshDbInfo)
            {
                schema.Tables = factory.GetReader().GetTables();
                save = true;
            }

            if (schema.Configs?.Count < 1 || options.AutomaticPopulation)
            {
                schema.Configs = autoPopulation.PopulateConfigs(schema.Configs, schema.Tables);
                save = true;
            }

            if (!save)
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
