namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class FullCreateGenerator : MethodGenerator
    {
        public override bool IsNeeded(Model model)
        {
            return model.IsInherited;
        }

        public override void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("private " + model.Name + " FullCreate(IDataReader reader)");
        }

        public override void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        { }
    }
}
