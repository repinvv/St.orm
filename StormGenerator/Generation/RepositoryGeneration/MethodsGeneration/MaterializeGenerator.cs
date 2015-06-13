namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class MaterializeGenerator : MethodGenerator
    {
        public override void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public List<" + model.Name + "> Materialize(IQueryable<" + parent.Name
                + "> query, ILoadService loadService)");
        }

        public override void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        { }
    }
}
