﻿namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using System;
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class SaveGenerator : IMethodGenerator
    {
        private readonly IdentityFinder identityFinder;

        public SaveGenerator(IdentityFinder identityFinder)
        {
            this.identityFinder = identityFinder;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void Save(" + model.Name + " entity, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if(!extension.PreSave(entity))");
            stringGenerator.Braces("return;");
            stringGenerator.AppendLine();

            identityFinder.WithIdentity(model, field =>
            {
                if (field.Type == typeof(Guid))
                {
                    stringGenerator.AppendLine("if(entity." + field.Name + " == Guid.Empty)");
                    stringGenerator.Braces("entity." + field.Name + " = Guid.NewGuid();");
                    stringGenerator.AppendLine();
                }
            });

            stringGenerator.AppendLine("SetMtoFields(entity);");
            stringGenerator.AppendLine("saves.Save<" + model.Name + ", " + model.Name + ">(entity);");
        }
    }
}
