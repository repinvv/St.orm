namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal class EmptyRelationsGenerator : IRelationsGenerator
    {
        public void GenerateSaveRelation(RelationField field, IStringGenerator stringGenerator)
        { }

        public void GenerateUpdateRelation(RelationField field, IStringGenerator stringGenerator)
        { }

        public void GenerateDeleteRelation(RelationField field, IStringGenerator stringGenerator)
        { }
    }
}
