namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.ModelGeneration.LazyGeneration;
    using StormGenerator.Generation.ModelGeneration.ModelPartsGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ModelGenerator
    {
        private readonly ModelPartsGeneratorFactory modelPartsGeneratorFactory;
        private readonly FileGenerator fileGenerator;
        private readonly EqualityGenerator equalityGenerator;
        private readonly UsingsGenerator usingsGenerator;
        private readonly ModelFieldsGenerator modelFieldsGenerator;

        public ModelGenerator(ModelPartsGeneratorFactory modelPartsGeneratorFactory,
            FileGenerator fileGenerator,
            EqualityGenerator equalityGenerator,
            UsingsGenerator usingsGenerator,
            ModelFieldsGenerator modelFieldsGenerator)
        {
            this.modelPartsGeneratorFactory = modelPartsGeneratorFactory;
            this.fileGenerator = fileGenerator;
            this.equalityGenerator = equalityGenerator;
            this.usingsGenerator = usingsGenerator;
            this.modelFieldsGenerator = modelFieldsGenerator;
        }

        public GeneratedFile GenerateModel(Model model)
        {
            return fileGenerator.GenerateFile(model.Name, stringGenerator => GenerateModelDefinition(model, stringGenerator));
        }

        private void GenerateModelDefinition(Model model, IStringGenerator stringGenerator)
        {
            if (model.IsStruct && model.RelationFields.ActiveAny())
            {
                throw new Exception("Struct types can't have navigation properties.");
            }

            var partGenerator = modelPartsGeneratorFactory.GetPartsGenerator(model);
            var usings = partGenerator.GetUsings(model);
            usingsGenerator.GenerateUsings(stringGenerator, usings);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine(partGenerator.GetDefinition(model));
            stringGenerator.Braces(() => GenerateContents(model, partGenerator, stringGenerator));
        }

        private void GenerateContents(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            modelFieldsGenerator.GenerateProperties(model, partGenerator, stringGenerator);
            stringGenerator.Region("Navigation properties", () => modelFieldsGenerator.GenerateLazyProperties(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Private fields", () => modelFieldsGenerator.GeneratePrivateFields(model, partGenerator, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Constructors", () => partGenerator.GenerateConstructors(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("ICloneable implementation", () => partGenerator.GenerateCloneableMembers(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Equality members", () => equalityGenerator.GenerateEqualityMembers(model, stringGenerator));
        }
    }
}
