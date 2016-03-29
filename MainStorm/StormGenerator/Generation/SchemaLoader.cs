namespace StormGenerator.Generation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using StormGenerator.AutomaticPopulation;
    using StormGenerator.DatabaseReading;
    using StormGenerator.Models;
    using StormGenerator.Models.Configs;
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
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };

            var schema = JsonConvert.DeserializeObject<Schema>(File.ReadAllText(schemaFile), settings) ?? new Schema();
            var save = false;
            if (schema.Tables == null || !schema.Tables.Any() || options.ForceRefreshDbInfo)
            {
                schema.Tables = factory.GetReader().GetTables();
                save = true;
            }

            schema.Configs = schema.Configs ?? new List<ModelConfig>();
            if (options.AutomaticPopulation)
            {
                var newConfigs = autoPopulation.PopulateConfigs(schema.Configs, schema.Tables);
                save = newConfigs.Count > schema.Configs.Count;
                schema.Configs = newConfigs;
            }

            if (!save)
            {
                return schema;
            }

            schema.Tables = factory.GetReader().GetTables();
            File.WriteAllText(schemaFile, JsonConvert.SerializeObject(schema, settings));
            return schema;
        }
    }
}
