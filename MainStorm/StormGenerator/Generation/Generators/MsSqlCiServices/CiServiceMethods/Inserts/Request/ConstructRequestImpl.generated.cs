//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices.CiServiceMethods.Inserts.Request
{
    using StormGenerator.Settings;
    using StormGenerator.Models.GenModels;
    using GeneratorHelpers;
    using System.Collections.Generic;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class ConstructRequestImpl
    {
        #region constructor
        Model model;
        List<Field> fields;
        string output;

        public ConstructRequestImpl(Model model, List<Field> fields, string output)
        {
            this.model = model;
            this.fields = fields;
            this.output = output;
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
            WriteLiteral(@"        private string insertRequestCache;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        private int insertCacheLength;");
            WriteLiteral(Environment.NewLine);
            Write(new SingleInsertRequest(model, fields, output).Execute());
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        private string ConstructInsertRequest(int count)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            if(insertCacheLength == count) return insertRequestCache;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var sb = new StringBuilder();");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            sb.AppendLine(""INSERT INTO ");
            Write(model.Table.Id);
            WriteLiteral(@""");");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            sb.AppendLine(""("");");
            WriteLiteral(Environment.NewLine);
            foreach (var line in fields.GetSelectLines())
            {
                WriteLiteral(@"            sb.AppendLine(""");
                Write(line);
                WriteLiteral(@""");");
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"            sb.AppendLine("")");
            Write(output);
            WriteLiteral(@" VALUES"");");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            AppendInsertKeys(sb, 0);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            for (int i = 1; i < count; i++)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                sb.AppendLine(""), "");");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                AppendInsertKeys(sb, i);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            ");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            sb.AppendLine("")"");");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            insertCacheLength = count;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            return insertRequestCache = sb.ToString();");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
