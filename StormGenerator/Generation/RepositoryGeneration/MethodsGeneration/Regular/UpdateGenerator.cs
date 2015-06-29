namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class UpdateGenerator : IMethodGenerator
    {
        private readonly Generics generics;

        public UpdateGenerator(Generics generics)
        {
            this.generics = generics;
        }

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
            stringGenerator.Braces("saves.Update" + generics.Line(model) + "(entity, existing);");
            stringGenerator.AppendLine("else");
            stringGenerator.Braces("saves.NoUpdate" + generics.Line(model) + "(entity, existing);");
        }
    }
}
