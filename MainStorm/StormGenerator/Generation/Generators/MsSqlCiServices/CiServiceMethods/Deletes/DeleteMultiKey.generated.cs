//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices.CiServiceMethods.Deletes
{
    using StormGenerator.Settings;
    using StormGenerator.Models.GenModels;
    using GeneratorHelpers;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class DeleteMultiKey
    {
        #region constructor
        Model model;
        GenOptions options;

        public DeleteMultiKey(Model model, GenOptions options)
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
            WriteLiteral(@"        public void Delete(");
            Write(model.Name);
            WriteLiteral(@" entity, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var parms = GetDeleteParameters(entity, 0);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            CiHelper.ExecuteNonQuery(SingleDeleteRequest, parms, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        public static int MaxAmountForGroupedDelete = 180;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        public void Delete(List<");
            Write(model.Name);
            WriteLiteral(@"> entities, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            if (entities.Count > MaxAmountForGroupedDelete)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                DeleteByTempTable(entities, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            else");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                GroupDelete(entities, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        public void DeleteByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            throw new CiException(""Entity ");
            Write(model.Name);
            WriteLiteral(@" has complex primary key, DeleteByPrimaryKey is not supported"");");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        #region delete methods");
            WriteLiteral(Environment.NewLine);
            Write(new SingleDeleteRequest(model, model.KeyFields).Execute());
            WriteLiteral(Environment.NewLine);
            Write(new DeleteByIdTempTable(model, options).Execute());
            WriteLiteral(Environment.NewLine);
            Write(new GroupDelete(model, model.KeyFields, options).Execute());
            WriteLiteral(@"        #endregion");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
