namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IStaticFileGenerator
    {
        string GetName(Options options);

        void GenerateContent(List<Model> models, Options options, IStringGenerator stringGenerator);
    }
}
