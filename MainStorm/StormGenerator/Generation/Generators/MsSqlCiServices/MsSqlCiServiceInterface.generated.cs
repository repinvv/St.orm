//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormGenerator.Generation.Generators.MsSqlCiServices
{
    using StormGenerator.Settings;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class MsSqlCiServiceInterface : NsFileGenerator
    {
        #region constructor
        GenOptions options;

        public MsSqlCiServiceInterface(GenOptions options)
            : base(options)
        {
            this.options = options;
        }
        #endregion

        public override string FileName => "ICIService";

        public override string Execute()
        {
            WriteLiteral(@"    using System.Data.SqlClient;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"    using System.Collections.Generic;");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"    ");
            Write(options.Visibility);
            WriteLiteral(@" interface ICiService<T>");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"    {");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        List<T> Get(string query, SqlParameter[] parms, SqlConnection conn, SqlTransaction trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        List<T> GetByPrimaryKey(object ids, SqlConnection conn, SqlTransaction trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        void Insert(List<T> entities, SqlConnection conn, SqlTransaction trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"        void Insert(T entity, SqlConnection conn, SqlTransaction trans);");
            WriteLiteral(Environment.NewLine);
            WriteLiteral(@"    }");
            WriteLiteral(Environment.NewLine);

            return ToString();
        }
    }
}
