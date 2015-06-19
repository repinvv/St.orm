namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

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
            if (model.IsStruct && model.RelationFields.AnyActive())
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
            stringGenerator.Region("Navigation properties", () => GenerateLazyProperties(model, parent, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Private fields", () => GeneratePrivateFields(model, parent, partGenerator, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Constructors", () => partGenerator.GenerateConstructors(model, parent, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("ICloneable implementation", () => partGenerator.GenerateCloneableMembers(model, stringGenerator));
        }

        private void GenerateLazyProperties(Model model, Model parent, IStringGenerator stringGenerator)
        {
            var relationFields = model.RelationFields.Active();
            var names = nameNormalizer.NormalizeNames(relationFields.Select(x => x.Name).ToList());
            for (int index = 0; index < relationFields.Count; index++)
            {
                var field = relationFields[index];
                field.Name = names[index];

                stringGenerator.AppendLine("public virtual " + fieldUtility.GetRelationFieldType(field) + " " + field.Name);
                stringGenerator.Braces(() =>
                {
                    stringGenerator.AppendLine("get");
                    stringGenerator.Braces(() => GenerateGetForProperty(field, index, model, parent, stringGenerator));
                    stringGenerator.AppendLine("set");
                    stringGenerator.Braces(() =>
                    {
                        stringGenerator.AppendLine("field" + index + " = value;");
                        stringGenerator.AppendLine("populated[" + index + "] = true;");
                    });
                });
                stringGenerator.AppendLine();
            }
        }

        private void GenerateGetForProperty(RelationField field, int index, Model model, Model parent, IStringGenerator stringGenerator)
        {
            stringGenerator.Region("property population", () => stringGenerator.AppendLine("return field" + index + ";"));
        }

        private void GenerateProperties(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            var names = nameNormalizer.NormalizeNames(model.MappingFields.Active().Select(x => x.Name).ToList());
            var fields = model.MappingFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                field.Name = names[index];
                partGenerator.GenerateMappingProperty(model, field, stringGenerator);
                stringGenerator.AppendLine();
            }
        }

        private void GeneratePrivateFields(Model model, Model parent, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            partGenerator.GeneratePrivateFields(model, parent, stringGenerator);
            var fields = model.RelationFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                stringGenerator.AppendLine("private " + fieldUtility.GetRelationFieldType(field) + " field" + index + ";");
            }
        }
    }
}
