namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.ModelGeneration.PropertyGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ModelGenerator
    {
        private readonly ModelPartsGeneratorFactory modelPartsGeneratorFactory;
        private readonly FieldUtility fieldUtility;
        private readonly NameNormalizer nameNormalizer;
        private readonly FileGenerator fileGenerator;

        public ModelGenerator(ModelPartsGeneratorFactory modelPartsGeneratorFactory,
            FieldUtility fieldUtility,
            NameNormalizer nameNormalizer,
            FileGenerator fileGenerator)
        {
            this.modelPartsGeneratorFactory = modelPartsGeneratorFactory;
            this.fieldUtility = fieldUtility;
            this.nameNormalizer = nameNormalizer;
            this.fileGenerator = fileGenerator;
        }

        public GeneratedFile GenerateModel(Model model, Model parent, Options options)
        {
            return fileGenerator.GenerateFile(model.Name, options, stringGenerator => GenerateModelDefinition(model, parent, stringGenerator));
        }

        private void GenerateModelDefinition(Model model, Model parent, IStringGenerator stringGenerator)
        {
            if (model.IsStruct && model.RelationFields.Any())
            {
                throw new Exception("Struct types can't have navigation properties.");
            }

            var partGenerator = modelPartsGeneratorFactory.GetPartsGenerator(model);
            partGenerator.GenerateUsings(model, stringGenerator);
            stringGenerator.AppendLine();
            partGenerator.GenerateDefinition(model, stringGenerator);
            stringGenerator.Braces(() => GenerateContents(model, parent, partGenerator, stringGenerator));
        }

        private void GenerateContents(Model model, Model parent, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            GenerateProperties(model, partGenerator, stringGenerator);
            GeneratePrivateFields(model, partGenerator, stringGenerator);
            stringGenerator.AppendLine();
            GenerateConstructors(model, partGenerator, stringGenerator);
            stringGenerator.AppendLine();
            GenerateLazyProperties(model, parent, stringGenerator);
        }

        private void GenerateLazyProperties(Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("#region Lazy properties");
            stringGenerator.AppendLine();
            for (int index = 0; index < model.RelationFields.Count; index++)
            {
                var field = model.RelationFields[index];
                stringGenerator.AppendLine("private " + fieldUtility.GetRelationFieldType(field) + " property" + index + " { get;set; }");
                stringGenerator.AppendLine();
            }

            stringGenerator.AppendLine("#endregion");
        }

        private void GenerateConstructors(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("#region Constructors");
            stringGenerator.AppendLine();
            partGenerator.GenerateConstructors(model, stringGenerator);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("#endregion");
        }

        private void GenerateProperties(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            // stringGenerator.AppendLine("#region Properties");
            // stringGenerator.AppendLine();
            var names = nameNormalizer.NormalizeNames(model.MappingFields.Select(x => x.Name).ToList());
            for (int index = 0; index < model.MappingFields.Count; index++)
            {
                var field = model.MappingFields[index];
                field.Name = names[index];
                partGenerator.GenerateMappingProperty(model, field, stringGenerator);
                stringGenerator.AppendLine();
            }

            names = nameNormalizer.NormalizeNames(model.RelationFields.Select(x => x.Name).ToList());
            for (int index = 0; index < model.RelationFields.Count; index++)
            {
                var field = model.RelationFields[index];
                field.Name = names[index];
                stringGenerator.AppendLine("public virtual " + fieldUtility.GetRelationFieldType(field) + " " + field.Name
                                           + " { get { return property" + index + "; } set { property" + index 
                                           + " = value; } }");
                stringGenerator.AppendLine();
            }

            // stringGenerator.AppendLine("#endregion");
        }

        private void GeneratePrivateFields(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("#region Private fields");
            stringGenerator.AppendLine();
            partGenerator.GeneratePrivateFields(model, stringGenerator);
            for (int index = 0; index < model.RelationFields.Count; index++)
            {
                var field = model.RelationFields[index];
                stringGenerator.AppendLine("private " + field.FieldModel.Name + " field" + index + ";");
            }

            stringGenerator.AppendLine();
            stringGenerator.AppendLine("#endregion");
        }
    }
}
