namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using System.Linq;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class SetMtoFieldsGenerator : IMethodGenerator 
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private void SetMtoFields(" + model.Name + " entity)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            bool first = true;
            foreach (var field in model.RelationFields.OfType<ManyToOneField>())
            {
                if (!first)
                {
                    stringGenerator.AppendLine();
                }

                stringGenerator.AppendLine("if(entity." + field.Name + " != null)");
                stringGenerator.Braces(() =>
                {
                    for (int i = 0; i < field.NearEndFields.Count; i++)
                    {
                        stringGenerator.AppendLine("entity." + field.NearEndFields[i].Name + " = entity."
                                                   + field.Name + "." + field.FarEndFields[i].Name + ";");
                    }
                });
                first = false;
            }
        }
    }
}
