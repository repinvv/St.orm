namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal interface IMethodGenerator
    {
        void GenerateSignature(Model model, IStringGenerator stringGenerator);

        void GenerateMethod(Model model, IStringGenerator stringGenerator);
    }
}
