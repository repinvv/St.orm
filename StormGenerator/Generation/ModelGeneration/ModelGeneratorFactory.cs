namespace StormGenerator.Generation.ModelGeneration.PropertyGeneration
{
    using StormGenerator.Models.Pregen;

    internal class ModelPartsGeneratorFactory
    {
        private readonly StructPartsGenerator structPartsGenerator;
        private readonly ClassPartsGenerator classPartsGenerator;

        public ModelPartsGeneratorFactory(StructPartsGenerator structPartsGenerator, ClassPartsGenerator classPartsGenerator)
        {
            this.structPartsGenerator = structPartsGenerator;
            this.classPartsGenerator = classPartsGenerator;
        }

        public IModelPartsGenerator GetPartsGenerator(Model model)
        {
            if (model.IsStruct)
            {
                return structPartsGenerator;
            }

            return classPartsGenerator;
        }
    }
}
