namespace StormGenerator.Generation
{
    using System;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;

    internal class FileGenerator
    {
        private readonly IStringGenerator stringGenerator;

        public FileGenerator(IStringGenerator stringGenerator)
        {
            this.stringGenerator = stringGenerator;
        }

        public GeneratedFile GenerateFile(string name, Options options, Action<IStringGenerator> generateAction)
        {
            stringGenerator.AppendLine(GenerationConstants.GenerationMark);
            stringGenerator.AppendLine("namespace " + options.OutputNamespace);
            stringGenerator.Braces(() => generateAction(stringGenerator));
            return new GeneratedFile
                   {
                       Name = name + ".cs",
                       Content = stringGenerator.ToString()
                   };
        }
    }
}
