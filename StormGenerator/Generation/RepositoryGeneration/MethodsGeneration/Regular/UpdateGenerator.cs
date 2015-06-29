namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class UpdateGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void Update(" + model.Name + " entity, " + model.Name + " existing, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if(!extension.PreUpdate(entity, existing))");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("Delete(entity, saves);");
                stringGenerator.AppendLine("return;");
            });
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("SetMtoFields(entity);");
            stringGenerator.AppendLine("if (EntityChanged(entity, existing))");
            stringGenerator.Braces("saves.Update<" + model.Name + ", " + model.Parent.Name + ">(entity, existing);");
            stringGenerator.AppendLine("else");
            stringGenerator.Braces("saves.NoUpdate<" + model.Name + ", " + model.Parent.Name + ">(entity, existing);");
        }
    }
}
