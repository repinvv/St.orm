//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices.CiServiceMethods.Updates
{
    using System.Collections.Generic;
    using StormGenerator.Settings;
    using StormGenerator.Models.GenModels;
    using GeneratorHelpers;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class SingleUpdateRequest
    {
        #region constructor
        Model model;
        GenOptions options;

        public SingleUpdateRequest(Model model, GenOptions options)
        {
            this.model = model;
            this.options = options;
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
            WriteLiteral(@"        private const string SingleUpdateRequest = ");
            WriteLiteral(@"@");
            WriteLiteral(@"""UPDATE ");
            Write(model.Table.Id);
            WriteLiteral(@" SET");
            WriteLiteral(Environment.NewLine);
            int i = 0;
            var fields = model.ValueFields();
            foreach (var field in fields)
            {
                WriteLiteral(@"    ");
                Write(field.Column.Name);
                WriteLiteral(@" = ");
                WriteLiteral(@"@");
                WriteLiteral(@"parm");
                Write(i++);
                WriteLiteral(@"i0");
                if (field != fields.Last())
                {
                    WriteLiteral(@",");
                }
                WriteLiteral(Environment.NewLine);
            }
            foreach (var field in model.KeyFields)
            {
                var start = field == model.KeyFields.First() ? "WHERE" : "AND";
                var end = field == model.KeyFields.Last() ? ";\";" : ",";
                WriteLiteral(@"  ");
                Write(start);
                WriteLiteral(@" ");
                Write(field.Column.Name);
                WriteLiteral(@" = ");
                WriteLiteral(@"@");
                WriteLiteral(@"parm");
                Write(i++);
                WriteLiteral(@"i0");
                Write(end);
                WriteLiteral(Environment.NewLine);
            }

            return executed = sb.ToString();
        }
    }
}