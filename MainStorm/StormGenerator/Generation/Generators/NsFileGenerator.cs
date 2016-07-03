namespace StormGenerator.Generation.Generators
{
    using System;
    using StormGenerator.Common;
    using StormGenerator.Settings;

    internal abstract class NsFileGenerator : FileGenerator// ns means namespace
    {
        private readonly GenOptions options;

        protected NsFileGenerator(GenOptions options)
        {
            this.options = options;
        }

        public override GeneratedFile GetFile()
        {
            Execute();
            var content = GenerationConstants.GenerationMark
                          + "namespace " + options.OutputNamespace + Environment.NewLine
                          + "{" + Environment.NewLine
                          + ToString()
                          + "}" + Environment.NewLine;

            return new GeneratedFile
                   {
                       Content = content,
                       Name = FileName + ".cs"
                   };
        }
    }
}
