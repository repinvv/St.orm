namespace StormGenerator.Generation
{
    using System;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;

    internal class FileGenerator
    {
        private readonly IStringGenerator stringGenerator;
        private readonly OptionsService options;

        public FileGenerator(IStringGenerator stringGenerator, OptionsService options)
        {
            this.stringGenerator = stringGenerator;
            this.options = options;
        }

        public GeneratedFile GenerateFile(string name, Action<IStringGenerator> generateAction)
        {
            stringGenerator.AppendLine(GenerationConstants.GenerationMark);
            stringGenerator.AppendLine("namespace " + options.Options.OutputNamespace);
            stringGenerator.Braces(() => generateAction(stringGenerator));
            return new GeneratedFile
                   {
                       Name = name + ".cs",
                       Content = stringGenerator.ToString()
                   };
        }
    }
}
