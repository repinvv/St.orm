namespace StormGenerator.Generation.ModelGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class EqualityGenerator
    {
        public void GenerateEqualityMembers(Model model, IStringGenerator stringGenerator)
        {
            var keyFields = model.MappingFields.Where(x => x.DbField.IsPrimaryKey).ToList();
            stringGenerator.AppendLine("public override bool Equals(object obj)");
            stringGenerator.Braces($"return Equals(obj as {model.Name});");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"public bool Equals({model.Name} other)");
            stringGenerator.Braces(() => GenerateEquals(keyFields, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public override int GetHashCode()");
            stringGenerator.Braces(() => GenerateGetHashCode(keyFields, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"public static bool operator ==({model.Name} left, {model.Name} right)");
            stringGenerator.Braces("return Equals(left, right);");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"public static bool operator !=({model.Name} left, {model.Name} right)");
            stringGenerator.Braces("return !Equals(left, right);");
        }

        private void GenerateGetHashCode(List<MappingField> keyFields, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("unchecked");
            stringGenerator.Braces(() => GenerateHashCreation(keyFields, stringGenerator));
        }

        private void GenerateHashCreation(List<MappingField> keyFields, IStringGenerator stringGenerator)
        {
            if (!keyFields.Any())
            {
                stringGenerator.AppendLine("return 0;");
                return;
            }

            for (int i = 0; i < keyFields.Count; i++)
            {
                string linestart = "hash = (hash * 397) ^ ";
                if (i == 0)
                {
                    linestart = "int hash = ";
                }

                if (i == keyFields.Count - 1)
                {
                    linestart = "return (hash * 397) ^ ";
                }

                if (keyFields.Count == 1)
                {
                    linestart = "return ";
                }

                stringGenerator.AppendLine(linestart + keyFields[i].Name + ".GetHashCode();");
            }
        }

        private void GenerateEquals(List<MappingField> keyFields, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if (ReferenceEquals(null, other))");
            stringGenerator.Braces("return false;");
            stringGenerator.AppendLine();

            if (!keyFields.Any())
            {
                stringGenerator.AppendLine("return false;");
                return;
            }

            for (int i = 0; i < keyFields.Count; i++)
            {
                string linestart = "equal = equal && ";
                if (i == 0)
                {
                    linestart = "bool equal = ";
                }

                if (i == keyFields.Count - 1)
                {
                    linestart = "return equal && ";
                }

                if (keyFields.Count == 1)
                {
                    linestart = "return ";
                }

                stringGenerator.AppendLine(linestart + keyFields[i].Name + " == other." + keyFields[i].Name + ";");
            }
        }
    }
}
