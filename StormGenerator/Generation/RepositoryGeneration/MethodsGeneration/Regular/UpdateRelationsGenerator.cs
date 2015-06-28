namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class UpdateRelationsGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void UpdateRelations(" + model.Name + " entity, " + model.Name
                                       + " existing, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        { }
    }
}
