namespace StormGenerator.Generation.ModelGeneration.ModelPartsGeneration
{
    using System.Collections.Generic;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class StructPartsGenerator : IModelPartsGenerator
    {
        private readonly FieldUtility fieldUtility;
        private readonly ClassPartsGenerator classPartsGenerator;
        private readonly DefinitionGenerator definitionGenerator;

        public StructPartsGenerator(FieldUtility fieldUtility,
            ClassPartsGenerator classPartsGenerator,
            DefinitionGenerator definitionGenerator)
        {
            this.fieldUtility = fieldUtility;
            this.classPartsGenerator = classPartsGenerator;
            this.definitionGenerator = definitionGenerator;
        }

        public IEnumerable<string> GetUsings(Model model)
        {
            return model.MappingFields.ActiveSelect(fieldUtility.GetUsing);
        }

        public string GetDefinition(Model model)
        {
            return definitionGenerator.GetModelDefinition(model, model, "struct", string.Empty);
        }

        public void GeneratePrivateFields(Model model, IStringGenerator stringGenerator)
        { }

        public void GenerateConstructors(Model model, IStringGenerator stringGenerator)
        { }

        public void GenerateCloneableMembers(Model model, IStringGenerator stringGenerator)
        { }
    }
}
