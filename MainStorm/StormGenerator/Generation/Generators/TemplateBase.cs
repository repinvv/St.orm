namespace StormGenerator.Generation.Generators
{
    using System.Text;
    using StormGenerator.Settings;

    internal abstract class TemplateBase<TData>
    {
        private readonly StringBuilder sb = new StringBuilder();

        public string FileName { get; set; }

        public RazorResolve Resolve { get; set; }

        public TData Model { protected get; set; }

        public GenerationOptions Options { protected get; set; }

        public abstract void Execute();

        protected void WriteLiteral(string text = null)
        {
            if (!string.IsNullOrEmpty(text))
            {
                sb.Append(text);
            }
        }

        protected void Write(object value = null)
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
    }
}
