namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Generation.StaticFilesGeneration.ContextGeneration;
    using StormGenerator.Models.Pregen;

    internal class StaticFilesGenerator
    {
        private readonly FileGenerator fileGenerator;
        private readonly IStaticFileGenerator[] generators;

        public StaticFilesGenerator(FileGenerator fileGenerator,
            ContextGenerator contextGenerator,
            DalRepositoryStorageGenerator dalRepositoryStorageGenerator,
            ContextExtensionGenerator contextExtensionGenerator,
            StormCommandsGenerator stormCommandsGenerator,
            AdoCommandsGenerator adoCommandsGenerator,
            ConnectionHandlerGenerator connectionHandlerGenerator)
        {
            this.fileGenerator = fileGenerator;
            generators = new IStaticFileGenerator[]
                         {
                             contextGenerator,
                             dalRepositoryStorageGenerator,
                             contextExtensionGenerator,
                             stormCommandsGenerator,
                             adoCommandsGenerator,
                             connectionHandlerGenerator
                         };
        }

        public List<GeneratedFile> GenerateStaticFiles(List<Model> models, Options options)
        {
            return generators.Select(x => fileGenerator
                                              .GenerateFile(x.GetName(options),
                                                            options,
                                                            stringGenerator => x.GenerateContent(models, options, stringGenerator))).ToList();
        }
    }
}
