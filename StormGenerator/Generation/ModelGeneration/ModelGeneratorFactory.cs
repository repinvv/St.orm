namespace StormGenerator.Generation.ModelGeneration
{
    using StormGenerator.Models.Pregen;

    internal class ModelPartsGeneratorFactory
    {
        private readonly StructPartsGenerator structPartsGenerator;
        private readonly AttributedClassPartsGenerator attributedClassPartsGenerator;
        private readonly PlainClassPartsGenerator plainClassPartsGenerator;

        public ModelPartsGeneratorFactory(StructPartsGenerator structPartsGenerator, 
            AttributedClassPartsGenerator attributedClassPartsGenerator,
            PlainClassPartsGenerator plainClassPartsGenerator)
        {
            this.structPartsGenerator = structPartsGenerator;
            this.attributedClassPartsGenerator = attributedClassPartsGenerator;
            this.plainClassPartsGenerator = plainClassPartsGenerator;
        }

        public IModelPartsGenerator GetPartsGenerator(Model model)
        {
            if (model.IsStruct)
            {
                return structPartsGenerator;
            }

            return attributedClassPartsGenerator;
        }
    }
}
