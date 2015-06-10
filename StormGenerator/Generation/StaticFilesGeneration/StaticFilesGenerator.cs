namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Generation.ContextGeneration;
    using StormGenerator.Generation.StaticFilesGeneration.ContextGeneration;
    using StormGenerator.Models.Pregen;

    internal class StaticFilesGenerator
    {
        private readonly FileGenerator fileGenerator;
        private readonly IStaticFileGenerator[] generators;

        public StaticFilesGenerator(FileGenerator fileGenerator,
            ContextGenerator contextGenerator,
            DalRepositoryStorageGenerator dalRepositoryStorageGenerator,
            ContextExtensionGenerator contextExtensionGenerator)
        {
            this.fileGenerator = fileGenerator;
            generators = new IStaticFileGenerator[]
                         {
                             contextGenerator,
                             dalRepositoryStorageGenerator,
                             contextExtensionGenerator
                         };
        }

        public IEnumerable<GeneratedFile> GenerateStaticFiles(List<Model> models, Options options)
        {
            return generators.Select(x => fileGenerator
                                              .GenerateFile(x.GetName(options),
                                                            options,
                                                            stringGenerator => x.GenerateContent(models, options, stringGenerator)));
        }
    }
}
