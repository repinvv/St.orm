namespace StormGenerator
{
    using System.Collections.Generic;
    using StormGenerator.Generation;
    using StormGenerator.Infrastructure;

    public class StormGeneration
    {
        public List<GeneratedFile> Generate(Options options)
        {
            var container = new Container();
            container.Get<OptionsService>().Options = options;
            var generator = container.Get<Generator>();
            return generator.Generate();
        }
    }
}
