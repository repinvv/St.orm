namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IMethodGenerator
    {
        void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator);

        void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator);
    }
}
