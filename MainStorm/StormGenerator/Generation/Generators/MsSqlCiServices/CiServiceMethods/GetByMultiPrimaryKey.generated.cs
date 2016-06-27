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
    using GeneratorHelpers;
    using StormGenerator.Models.GenModels;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class GetByMultiPrimaryKey
    {
        #region constructor
        Model model;

        public GetByMultiPrimaryKey(Model model)
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
            WriteLiteral(@"        public List<");
            Write(model.Name);
            WriteLiteral(@"> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var idsArray = (object[])ids;");
            WriteLiteral(Environment.NewLine);
            var i = 0;
            foreach (var key in model.KeyFields)
            {
                WriteLiteral(@"            var key");
                Write(i);
                WriteLiteral(@" = (");
                Write(key.Column.CsTypeName);
                WriteLiteral(@"[])idsArray[");
                Write(i++);
                WriteLiteral(@"];");
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"            using (new ConnectionHandler(conn))");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                var table = CiHelper.CreateTempTableName();");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                CreateIdTempTable(table, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                var dataReader = new KeyDataReader(");
            Write(model.JoinKeyNames());
            WriteLiteral(@");");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                CiHelper.BulkInsert(dataReader, table, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                var sql = ");
            WriteLiteral(@"@");
            WriteLiteral(@"""select ");
            WriteLiteral(Environment.NewLine);
            foreach (var line in model.GetFieldSelectLines("e."))
            {
                WriteLiteral(@"                ");
                Write(line);
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"				from ");
            Write(model.Table.Id);
            WriteLiteral(@" e");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                inner join "" + table + ");
            WriteLiteral(@"@");
            WriteLiteral(@""" t on ");
            WriteLiteral(Environment.NewLine);
            foreach (var line in model.GetKeyEqualityLines("e.", "t.", "\";"))
            {
                WriteLiteral(@"                ");
                Write(line);
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"                var result = CiHelper.ExecuteSelect(sql, new SqlParameter[0], ReadEntities, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                CiHelper.DropTable(table, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                return result;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"		private void CreateIdTempTable(string table, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            var sql = ""CREATE TABLE "" + table + ");
            WriteLiteral(@"@");
            WriteLiteral(@"""(");
            WriteLiteral(Environment.NewLine);
            foreach (var field in model.KeyFields)
            {
                WriteLiteral(@"                ");
                Write(field.Column.Definition);
                if (field != model.KeyFields.Last())
                {
                    WriteLiteral(@",");
                }
                WriteLiteral(Environment.NewLine);
            }
            WriteLiteral(@"                )"";");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            CiHelper.ExecuteNonQuery(sql, new SqlParameter[0], conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
