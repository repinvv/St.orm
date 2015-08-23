namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IStaticFileGenerator
    {
        string GetName();

        void GenerateContent(List<Model> models, IStringGenerator stringGenerator);
    }
}
