//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices.CiServiceMethods
{
    using StormGenerator.Models.GenModels;
    using System.Collections.Generic;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class SequenceInsertKeys
    {
        #region constructor
        List<Field> fields;

        public SequenceInsertKeys(List<Field> fields)
        {
            this.fields = fields;
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
            WriteLiteral(@"        private void AppendInsertKeys(StringBuilder sb, int i)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            var i = 0; 
            foreach (var field in fields)
            {
                var start = field == fields.First() ? "(" : ","; 
                if (field.Column.IsPrimaryKey && field.Column.Sequence != null)
                {
                    WriteLiteral(@"                sb.Append(""");
                    Write(start);
                    WriteLiteral(@" NEXT VALUE FOR ");
                    Write(field.Column.Sequence);
                    WriteLiteral(@""");");
                    WriteLiteral(Environment.NewLine);
                }
                else
                {
                    WriteLiteral(@"                sb.Append(""");
                    Write(start);
                    WriteLiteral(@" ");
                    WriteLiteral(@"@");
                    WriteLiteral(@"parm");
                    Write(i++);
                    WriteLiteral(@"i""); sb.Append(i);");
                    WriteLiteral(Environment.NewLine);
                }
            }
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
