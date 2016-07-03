//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices.CiServiceMethods.Insert
{
    using StormGenerator.Settings;
    using StormGenerator.Models.GenModels;
    using GeneratorHelpers;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class RangeInsertWithKey
    {
        #region constructor
        Model model;
        GenOptions options;

        public RangeInsertWithKey(Model model, GenOptions options)
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
            WriteLiteral(@"        private void RangeInsert(List<");
            Write(model.Name);
            WriteLiteral(@"> entities, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            if(insertCacheLength != entities.Count)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                insertRequestCache = ConstructInsertRequest(entities.Count);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                insertCacheLength = entities.Count;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            int i = 0;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var parms = entities.SelectMany(x => GetInsertParameters(x, i++)).ToArray();");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var sql = ConstructInsertRequest(entities.Count);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            CiHelper.ExecuteSelect(sql, parms, reader => ReadKey(reader, entities), conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        private List<");
            Write(model.Name);
            WriteLiteral(@"> ReadKey(SqlDataReader reader, List<");
            Write(model.Name);
            WriteLiteral(@"> entities)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            int i = 0;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            while (reader.Read())");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                entities[i++].");
            Write(model.KeyFields[0].Name);
            WriteLiteral(@" = ");
            Write(model.KeyFields[0].GetReaderMethod(0));
            WriteLiteral(@";");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            return entities;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
