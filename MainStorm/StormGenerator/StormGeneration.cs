namespace StormGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using StormGenerator.Generation;
    using StormGenerator.Infrastructure;
    using StormGenerator.Settings;

    public static class StormGeneration
    {
        public static List<GeneratedFile> Generate(string optionsFile, string schemaFile)
        {
            var options = JsonConvert.DeserializeObject<Options>(File.ReadAllText(optionsFile));
            var container = new Container(options);
            return container.Get<Generator>().Generate(schemaFile);
        }
    }
}
