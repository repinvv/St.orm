//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices.CiServiceMethods.Inserts
{
    using StormGenerator.Models.GenModels;
    using System.Collections.Generic;
    using GeneratorHelpers;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class InsertParameters
    {
        #region constructor
        Model model;
        List<Field> fields;

        public InsertParameters(Model model, List<Field> fields)
        {
            this.model = model;
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
            WriteLiteral(@"        private SqlParameter[] GetInsertParameters(");
            Write(model.Name);
            WriteLiteral(@" entity, int i)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            return new[]");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            var i = 0; 
            foreach (var field in fields)
            {
                WriteLiteral(@"                new SqlParameter(""parm");
                Write(i++);
                WriteLiteral(@"i"" + i, SqlDbType.");
                Write(field.GetSqlType());
                WriteLiteral(@")");
                WriteLiteral(Environment.NewLine);
                WriteLiteral(@"                { Value = entity.");
                Write(field.Name);
                if (field.Column.IsNullable)
                {
                    WriteLiteral(@" ?? (object)DBNull.Value");
                }
                WriteLiteral(@" },");
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"            };");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
