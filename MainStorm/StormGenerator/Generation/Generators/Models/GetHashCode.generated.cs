//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.Models
{
    using StormGenerator.Models.GenModels;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class GetHashCode
    {
        #region constructor
        Model model;

        public GetHashCode(Model model)
        {
            this.model = model;
        }
        #endregion

        #region basic members
        private readonly StringBuilder sb = new StringBuilder();
        private string executed;

        private void WriteLiteral(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                sb.Append(text);
            }
        }

        private void Write(object value)
        {
            if (value != null)
            {
                sb.Append(value);
            }
        }

        public override string ToString()
        {
            return executed;
        }
        #endregion

        public string Execute()
        {
            var keyfields = model.KeyFields.Any() ? model.KeyFields : model.Fields;
            WriteLiteral("        public override int GetHashCode()");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {");
            WriteLiteral(Environment.NewLine);
            if (keyfields.Count == 1)
            {
                WriteLiteral("            return ");
                Write(keyfields[0].Name);
                WriteLiteral(".GetHashCode();");
                WriteLiteral(Environment.NewLine);
            }
            else
            {
                WriteLiteral("            return new int[]");
                WriteLiteral(Environment.NewLine);
                WriteLiteral("            {");
                WriteLiteral(Environment.NewLine);
                foreach (var field in keyfields)
                {
                    WriteLiteral("                ");
                    Write(field.Name);
                    WriteLiteral(".GetHashCode(),");
                    WriteLiteral(Environment.NewLine);
                }
                WriteLiteral("            ");
                WriteLiteral("}.CombineHashcodes();");
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
