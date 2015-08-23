namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal interface IRelationsGenerator
    {
        void GenerateSaveRelation(RelationField field, IStringGenerator stringGenerator);

        void GenerateUpdateRelation(RelationField field, IStringGenerator stringGenerator);

        void GenerateDeleteRelation(RelationField field, IStringGenerator stringGenerator);
    }
}
