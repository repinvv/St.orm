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
        private readonly IStringGenerator stringGenerator;
        private readonly ModelPartsGeneratorFactory modelPartsGeneratorFactory;
        private readonly FieldUtility fieldUtility;

        public ModelGenerator(IStringGenerator stringGenerator, 
            ModelPartsGeneratorFactory modelPartsGeneratorFactory,
            FieldUtility fieldUtility)
        {
            this.stringGenerator = stringGenerator;
            this.modelPartsGeneratorFactory = modelPartsGeneratorFactory;
            this.fieldUtility = fieldUtility;
        }

        public GeneratedFile GenerateModel(Model model, string outputNamespace)
        {
            return new GeneratedFile
                   {
                       Name = model.Name,
                       Content = GenerateModelContent(model, outputNamespace)
                   };
        }

        private string GenerateModelContent(Model model, string outputNamespace)
        {
            if (model.IsStruct && model.RelationFields.Any())
            {
                throw new Exception("Struct types can't have navigation properties.");
            }

            stringGenerator.AppendLine("namespace " + outputNamespace);
            stringGenerator.Braces(() => GenerateModelDefinition(model));
            return stringGenerator.ToString();
        }

        private void GenerateModelDefinition(Model model)
        {
            var partGenerator = modelPartsGeneratorFactory.GetPartsGenerator(model);
            partGenerator.GenerateUsings(model, stringGenerator);
            stringGenerator.AppendLine();
            partGenerator.GenerateDefinition(model, stringGenerator);
            stringGenerator.Braces(() => GenerateContents(model, partGenerator));
        }

        private void GenerateContents(Model model, IModelPartsGenerator partGenerator)
        {
            GenerateProperties(model, partGenerator);
            GeneratePrivateFields(model, partGenerator);
            stringGenerator.AppendLine();
            GenerateConstructors(model, partGenerator);
            stringGenerator.AppendLine();
            GenerateLazyProperties(model);
        }

        private void GenerateLazyProperties(Model model)
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

        private void GenerateConstructors(Model model, IModelPartsGenerator partGenerator)
        {
            stringGenerator.AppendLine("#region Constructors");
            stringGenerator.AppendLine();
            partGenerator.GenerateConstructors(model, stringGenerator);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("#endregion");
        }

        private void GenerateProperties(Model model, IModelPartsGenerator partGenerator)
        {
            // stringGenerator.AppendLine("#region Properties");
            // stringGenerator.AppendLine();
            for (int index = 0; index < model.MappingFields.Count; index++)
            {
                var field = model.MappingFields[index];
                partGenerator.GenerateMappingProperty(field, index, stringGenerator);
                stringGenerator.AppendLine();
            }

            for (int index = 0; index < model.RelationFields.Count; index++)
            {
                var field = model.RelationFields[index];

                stringGenerator.AppendLine("public virtual " + fieldUtility.GetRelationFieldType(field) + " " + field.Name 
                    + " { get { return property" + index + "; } set { property" + index + " = value; } }");
                stringGenerator.AppendLine();
            }

            // stringGenerator.AppendLine("#endregion");
        }

        private void GeneratePrivateFields(Model model, IModelPartsGenerator partGenerator)
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
