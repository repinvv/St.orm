namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class EntityChangedGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public bool EntityChanged(" + model.Name + " entity, " + model.Name + " existing)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            var fields = model.MappingFields.Active(x => !x.DbField.IsPrimaryKey);
            var ext = "return extension.ExtendEntityChanged(entity, existing)";
            if (fields.Count == 0)
            {
                stringGenerator.AppendLine(ext + ";");
                return;
            }

            stringGenerator.AppendLine(ext);
            stringGenerator.PushIndent();
            for (int i = 0; i < fields.Count; i++)
            {
                var semicolon = i == fields.Count - 1 ? ";" : string.Empty;
                var field = fields[i];
                stringGenerator.AppendLine("|| entity." + field.Name + " != existing." + field.Name + semicolon);
            }

            stringGenerator.PopIndent();
        }
    }
}
