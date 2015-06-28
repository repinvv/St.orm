namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class DeleteRelationsGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private void DeleteRelations(" + model.Name + " entity, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        { }
    }
}
