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
    using Request;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class IdentityInsert
    {
        #region constructor
        Model model;
        GenOptions options;

        public IdentityInsert(Model model, GenOptions options)
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
             var parmCount = model.Fields.Count - 1;
   var maxItems = Math.Min(options.MaxInsertItems, options.MaxSqlParms / parmCount);
 
            WriteLiteral(@"        public void Insert(List<");
            Write(model.Name);
            WriteLiteral(@"> entities, SqlConnection conn, SqlTransaction trans)");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        {        ");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            foreach(var group in entities.SplitInGroupsBy(");
            Write(maxItems);
            WriteLiteral(@"))");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"                GroupInsert(group, conn, trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"            }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        }");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            Write(new SingleInsert(model).Execute());
            WriteLiteral(Environment.NewLine);
            var fields = model.ValueFields();
            WriteLiteral(@"        #region range insert methods");
            WriteLiteral(Environment.NewLine);
            if (!model.IsStruct)
            {
                Write(new GroupInsertWithKey(model, options).Execute());
                WriteLiteral(Environment.NewLine);
                Write(new ConstructRequestWithOutput(model, fields).Execute());
                WriteLiteral(Environment.NewLine);
                Write(new AppendInsertKeys(fields).Execute());
            }
            else
            {
                Write(new GroupInsert(model, options).Execute());
                WriteLiteral(Environment.NewLine);
                Write(new ConstructRequest(model, fields).Execute());
                WriteLiteral(Environment.NewLine);
                Write(new AppendInsertKeys(fields).Execute());
            }
            WriteLiteral(Environment.NewLine);
            Write(new InsertParameters(model, fields).Execute());
            WriteLiteral(@"        #endregion");
            WriteLiteral(Environment.NewLine);

            return executed = sb.ToString();
        }
    }
}
