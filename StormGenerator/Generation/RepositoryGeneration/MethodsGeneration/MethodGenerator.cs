namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal abstract class MethodGenerator
    {
        public virtual bool IsNeeded(Model model)
        {
            return true;
        }

        public abstract void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator);

        public abstract void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator);
    }
}
