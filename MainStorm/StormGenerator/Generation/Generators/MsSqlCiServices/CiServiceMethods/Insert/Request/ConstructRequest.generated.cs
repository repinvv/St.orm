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
    using GeneratorHelpers;
    using System.Collections.Generic;
    using System;
    using System.Text;
    using System.Linq;

    [System.CodeDom.Compiler.GeneratedCode("SharpRazor", "1.0.0.0")]
    internal class ConstructRequest
    {
        #region constructor
        Model model;
        List<Field> fields;

        public ConstructRequest(Model model, List<Field> fields)
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
            Write(new ConstructRequestImpl(model, fields, string.Empty).Execute());

            return executed = sb.ToString();
        }
    }
}
