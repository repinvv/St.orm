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
    using StormGenerator.Settings;
    using StormGenerator.Models.GenModels;
    using GeneratorHelpers;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class GroupInsert
    {
        #region constructor
        Model model;
        GenOptions options;

        public GroupInsert(Model model, GenOptions options)
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
            WriteLiteral(@"        private void GroupInsert(List<");
            Write(model.Name);
            WriteLiteral(@"> entities, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var parms = entities.SelectMany((x, i) => GetInsertParameters(x, i)).ToArray();");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var sql = ConstructInsertRequest(entities.Count);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            CiHelper.ExecuteNonQuery(sql, parms, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
