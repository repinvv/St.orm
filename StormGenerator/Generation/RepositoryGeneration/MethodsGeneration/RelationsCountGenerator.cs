﻿namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class RelationsCountGenerator : MethodGenerator
    {
        public override void GenerateSignature(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public int RelationsCount()");
        }

        public override void GenerateMethod(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("return extension.RelationsCount() ?? " + model.RelationFields.Count + ";");
        }
    }
}
