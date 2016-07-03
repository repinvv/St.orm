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
    using StormGenerator.Settings;
    using StormGenerator.Models.GenModels;
    using StormGenerator.Generation.Generators.GeneratorHelpers;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class ReadEntities
    {
        #region constructor
        Model model;

        public ReadEntities(Model model)
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
            WriteLiteral(@"        private List<");
            Write(model.Name);
            WriteLiteral(@"> ReadEntities(SqlDataReader reader)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var list = new List<");
            Write(model.Name);
            WriteLiteral(@">();");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            while (reader.Read())");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                var entity = new ");
            Write(model.Name);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                {");
            WriteLiteral(Environment.NewLine);
            int i = 0;
            foreach (var field in model.Fields.Where(x=>x.IsEnabled))
            {
                WriteLiteral(@"                    ");
                Write(field.Name);
                WriteLiteral(@" = ");
                Write(field.GetReaderMethod(i++));
                WriteLiteral(@",");
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"                };");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                list.Add(entity);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            return list;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
