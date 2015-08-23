namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class DeleteGenerator : IMethodGenerator
    {
        private readonly Generics generics;

        public DeleteGenerator(Generics generics)
        {
            this.generics = generics;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"public void Delete({model.Name} entity, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if(!extension.PreDelete(entity))");
            stringGenerator.Braces("return;");
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("DeleteRelations(entity, saves);");
            stringGenerator.AppendLine($"saves.Delete{generics.Line(model)}(entity);");
        }
    }
}
