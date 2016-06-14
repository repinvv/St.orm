namespace StormGenerator.Generation.Generators
{
    using System.Text;
    using StormGenerator.Common;

    internal abstract class FileGenerator
    {
        private readonly StringBuilder sb = new StringBuilder();

        protected void WriteLiteral(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                sb.Append(text);
            }
        }

        protected void Write(object value)
        {
            if (value != null)
            {
                sb.Append(value);
            }
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public abstract string FileName { get; }

        public abstract string Execute();

        public GeneratedFile GetFile()
        {
            Execute();
            return new GeneratedFile
                   {
                       Content = GenerationConstants.GenerationMark + sb.ToString(),
                       Name = FileName + ".cs"
                   };
        }
    }
}
