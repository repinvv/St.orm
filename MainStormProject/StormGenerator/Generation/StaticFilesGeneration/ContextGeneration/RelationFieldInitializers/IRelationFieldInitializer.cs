namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration.RelationFieldInitializers
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal interface IRelationFieldInitializer
    {
        void InitializeRelationField(RelationField field, IStringGenerator stringGenerator);
    }
}
