﻿namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class FullCreateGenerator : IMethodGenerator
    {
        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"private {model.Name} FullCreate(IDataReader reader)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        { }
    }
}
