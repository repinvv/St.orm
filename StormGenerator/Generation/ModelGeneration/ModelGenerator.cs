namespace StormGenerator.Generation.ModelGeneration
{
    using System;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Generation.ModelGeneration.LazyGeneration;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ModelGenerator
    {
        private readonly ModelPartsGeneratorFactory modelPartsGeneratorFactory;
        private readonly FieldUtility fieldUtility;
        private readonly NameNormalizer nameNormalizer;
        private readonly FileGenerator fileGenerator;
        private readonly LazyGetGenerator lazyGetGenerator;
        private readonly EqualityGenerator equalityGenerator;

        public ModelGenerator(ModelPartsGeneratorFactory modelPartsGeneratorFactory,
            FieldUtility fieldUtility,
            NameNormalizer nameNormalizer,
            FileGenerator fileGenerator,
            LazyGetGenerator lazyGetGenerator,
            EqualityGenerator equalityGenerator)
        {
            this.modelPartsGeneratorFactory = modelPartsGeneratorFactory;
            this.fieldUtility = fieldUtility;
            this.nameNormalizer = nameNormalizer;
            this.fileGenerator = fileGenerator;
            this.lazyGetGenerator = lazyGetGenerator;
            this.equalityGenerator = equalityGenerator;
        }

        public GeneratedFile GenerateModel(Model model, Options options)
        {
            return fileGenerator.GenerateFile(model.Name, options, stringGenerator => GenerateModelDefinition(model, stringGenerator));
        }

        private void GenerateModelDefinition(Model model, IStringGenerator stringGenerator)
        {
            if (model.IsStruct && model.RelationFields.ActiveAny())
            {
                throw new Exception("Struct types can't have navigation properties.");
            }

            var partGenerator = modelPartsGeneratorFactory.GetPartsGenerator(model);
            partGenerator.GenerateUsings(model, stringGenerator);
            stringGenerator.AppendLine();
            partGenerator.GenerateDefinition(model, stringGenerator);
            stringGenerator.Braces(() => GenerateContents(model, partGenerator, stringGenerator));
        }

        private void GenerateContents(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            GenerateProperties(model, partGenerator, stringGenerator);
            stringGenerator.Region("Navigation properties", () => GenerateLazyProperties(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Private fields", () => GeneratePrivateFields(model, partGenerator, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Constructors", () => partGenerator.GenerateConstructors(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("ICloneable implementation", () => partGenerator.GenerateCloneableMembers(model, stringGenerator));
            stringGenerator.AppendLine();
            stringGenerator.Region("Equality members", () => equalityGenerator.GenerateEqualityMembers(model, stringGenerator));
        }

        private void GenerateLazyProperties(Model model, IStringGenerator stringGenerator)
        {
            var relationFields = model.RelationFields.Active();
            var names = nameNormalizer.NormalizeNames(relationFields.Select(x => x.Name).ToList());
            for (int index = 0; index < relationFields.Count; index++)
            {
                var field = relationFields[index];
                field.Name = names[index];

                stringGenerator.AppendLine("public virtual " + fieldUtility.GetRelationFieldType(field) + " " + field.Name);
                stringGenerator
                    .Braces(() => stringGenerator
                                .Region("implementation", () => GenerateLazyProperty(field, index, model, stringGenerator)));
                stringGenerator.AppendLine();
            }
        }

        private void GenerateLazyProperty(RelationField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("get");
            stringGenerator.Braces(() => lazyGetGenerator.GenerateLazyGet(field, index, model, stringGenerator));
            stringGenerator.AppendLine("set");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine("field" + index + " = value;");
                stringGenerator.AppendLine("populated[" + index + "] = true;");
            });
        }

        private void GenerateProperties(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            var names = nameNormalizer.NormalizeNames(model.MappingFields.ActiveSelect(x => x.Name).ToList());
            var fields = model.MappingFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                field.Name = names[index];
                partGenerator.GenerateMappingProperty(model, field, stringGenerator);
                stringGenerator.AppendLine();
            }
        }

        private void GeneratePrivateFields(Model model, IModelPartsGenerator partGenerator, IStringGenerator stringGenerator)
        {
            partGenerator.GeneratePrivateFields(model, stringGenerator);
            var fields = model.RelationFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                stringGenerator.AppendLine("private " + fieldUtility.GetRelationFieldType(field) + " field" + index + ";");
            }
        }
    }
}
