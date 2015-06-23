namespace StormGenerator
{
    using System.Collections.Generic;
    using StormGenerator.Generation;
    using StormGenerator.Infrastructure;

    public class StormGeneration
    {
        public List<GeneratedFile> Generate(Options options)
        {
            var generator = new Container().Get<Generator>();
            return generator.Generate(options);
        }
    }
}
