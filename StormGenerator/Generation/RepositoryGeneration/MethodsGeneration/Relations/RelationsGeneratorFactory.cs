namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
    using StormGenerator.Models.Pregen.Relation;

    internal class RelationsGeneratorFactory
    {
        private readonly ManyToManyRelationsGenerator manyToManyRelationsGenerator;
        private readonly OneToManyRelationsGenerator oneToManyRelationsGenerator;
        private readonly EmptyRelationsGenerator emptyRelationsGenerator;

        public RelationsGeneratorFactory(ManyToManyRelationsGenerator manyToManyRelationsGenerator,
            OneToManyRelationsGenerator oneToManyRelationsGenerator,
            EmptyRelationsGenerator emptyRelationsGenerator)
        {
            this.manyToManyRelationsGenerator = manyToManyRelationsGenerator;
            this.oneToManyRelationsGenerator = oneToManyRelationsGenerator;
            this.emptyRelationsGenerator = emptyRelationsGenerator;
        }

        public IRelationsGenerator GetRelationsGenerator(RelationField relationField)
        {
            if (relationField is OneToManyField)
            {
                return oneToManyRelationsGenerator;
            }

            if (relationField is ManyToManyField)
            {
                return manyToManyRelationsGenerator;
            }

            return emptyRelationsGenerator;
        }
    }
}
