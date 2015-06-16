namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class CloneGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + model.Name + " Clone(" + model.Name + " source)");
        }

        public void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var clone = (source as ICloneable<" + model.Name + ">).Clone();");
            stringGenerator.AppendLine("extension.ExtendClone(clone, source);");
            stringGenerator.AppendLine("return clone;");
        }
    }
}
