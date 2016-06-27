//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices
{
    using StormGenerator.Settings;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class SingleKeyDataReader : FileGenerator
    {
        #region constructor
        GenOptions options;

        public SingleKeyDataReader(GenOptions options)
        {
            this.options = options;
        }
        #endregion

        public override string FileName => "SingleKeyDataReader";

        public override string Execute()
        {
            WriteLiteral("namespace ");
            Write(options.OutputNamespace);
            WriteLiteral(Environment.NewLine);
            WriteLiteral("{");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("    ");
            Write(options.Visibility);
            WriteLiteral(" class SingleKeyDataReader<T> : BaseDataReader");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("    {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        private readonly T[] keys;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        public SingleKeyDataReader(T[] keys) : base(keys.Length)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            this.keys = keys;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        public override object GetValue(int i)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            return keys[current];");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        public override int FieldCount { get { return 1; } }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("    }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("}");
            WriteLiteral(Environment.NewLine);

            return ToString();
        }
    }
}