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
            var models = generator.Generate(options);
            models = models
                     ?? new List<GeneratedFile>
                        {
                            new GeneratedFile
                            {
                                Name = "file1",
                                Content = "file1"
                            },
                            new GeneratedFile
                            {
                                Name = "file2",
                                Content = "file2"
                            }
                        };
            return models;
        }
    }
}
