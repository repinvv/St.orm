namespace StormGenerator
{
    using System.Collections.Generic;
    using StormGenerator.Generation;
    using StormGenerator.Infrastructure;

    public class StormGeneration
    {
        public List<GeneratedFile> Generate(string edmx, string config, string outputNamespace)
        {
            return new Container().Get<Generator>()
                                  .Generate(edmx, config, outputNamespace) ?? new List<GeneratedFile>
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
        }
    }
}
