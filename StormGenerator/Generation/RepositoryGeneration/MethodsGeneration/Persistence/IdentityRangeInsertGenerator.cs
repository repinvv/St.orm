namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Persistence
{
    using System;
    using System.Linq;
    using StormGenerator.Generation.Common;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class IdentityRangeInsertGenerator : IMethodGenerator
    {
        private readonly MaterializerLineGenerator materializerLineGenerator;
        private readonly TypeService typeService;

        public IdentityRangeInsertGenerator(MaterializerLineGenerator materializerLineGenerator, TypeService typeService)
        {
            this.materializerLineGenerator = materializerLineGenerator;
            this.typeService = typeService;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private void RangeInsert(List<" + model.Name
                                       + "> entities, DbConnection connection, DbTransaction transaction)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            var idField = model.MappingFields.First(x => x.DbField.IsIdentity);
            var fields = model.MappingFields.Active(x => x != idField);
            stringGenerator.AppendLine("int i;");
            stringGenerator.AppendLine("var sb = new StringBuilder();");
            stringGenerator.AppendLine("sb.AppendLine(\"INSERT INTO " + model.DbModel.Id + "\");");
            var fieldsString = string.Join(", ", fields.Select(x => x.DbField.Name));
            stringGenerator.AppendLine("sb.AppendLine(\"    (" + fieldsString + ")\");");
            stringGenerator.AppendLine("sb.AppendLine(\"OUTPUT inserted." + idField.DbField.Name + "\");");
            stringGenerator.AppendLine("sb.AppendLine(\"VALUES\");");
            int i = 1;
            fieldsString = string.Join(", ", fields.Select(x => "@parm" + i++ + "i0"));
            stringGenerator.AppendLine("sb.AppendLine(\"    (" + fieldsString + ")\");");
            stringGenerator.AppendLine("for (i = 1; i < entities.Count; i++)");
            i = 1;
            fieldsString = string.Join(", ", fields.Select(x => "@parm" + i++ + "i\" + i + \""));
            stringGenerator.Braces("sb.AppendLine(\"   ,(" + fieldsString + ")\");");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("var parameters = new List<SqlParameter>(entities.Count*" + fields.Count + ");");
            stringGenerator.AppendLine("for (i = 0; i < entities.Count; i++)");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("var entity = entities[i];");
                for (i = 0; i < fields.Count; i++)
                {
                    var dbnull = typeService.CanBeNull(fields[i].Type) ? " ?? (object)DBNull.Value" : string.Empty;
                    stringGenerator.AppendLine("parameters.Add(new SqlParameter(\"@parm" + (i + 1)
                                               + "i\" + i, entity." + fields[i].Name + dbnull + "));");
                }
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("i = 0;");
            var matLine = materializerLineGenerator.GenerateMaterializerLine("r", idField.Type, 0);
            stringGenerator.AppendLine("AdoCommands.RunCommand(sb.ToString(), parameters.ToArray(), connection, transaction, r => entities[i++]." +
                                       idField.Name + " = " + matLine + ");");
        }
    }
}
