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
    internal class ClassEquality
    {
        #region constructor
        Model model;

        public ClassEquality(Model model)
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
            WriteLiteral("        public override bool Equals(object obj)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            return Equals(obj as ");
            Write(model.Name);
            WriteLiteral(");		  ");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("			  ");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        public bool Equals(");
            Write(model.Name);
            WriteLiteral(" other)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {		");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            if (other == null)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("                return false;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral("			if(ReferenceEquals(this, other))");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("			{");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("			    return true;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("			}");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            Write(new FieldsComparision(model).Execute());
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            Write(new GetHashCode(model).Execute());
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        public static bool operator ==(");
            Write(model.Name);
            WriteLiteral(" left, ");
            Write(model.Name);
            WriteLiteral(" right)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            return left != null && left.Equals(right);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        public static bool operator !=(");
            Write(model.Name);
            WriteLiteral(" left, ");
            Write(model.Name);
            WriteLiteral(" right)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("            return !(left == right);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral("        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
