namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class MaterializeGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public List<" + model.Name + "> Materialize(IQueryable<" + model.Parent.Name
                + "> query, ILoadService loadService)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var context = loadService.Context;");
            var method = model.IsInherited ? "FullCreate" : "Create";
            stringGenerator.AppendLine("return AdoCommands.Materialize(query,");
            stringGenerator.PushIndent();
            stringGenerator.AppendLine("reader => " + method + "(reader, query, loadService),");
            stringGenerator.AppendLine("context.Connection,");
            stringGenerator.AppendLine("context.Transaction);");
            stringGenerator.PopIndent();
        }
    }
}
