namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration.RelationFieldInitializers
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToManyIgnoreInitializer : IRelationFieldInitializer
    {
        public void InitializeRelationField(RelationField field, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(".Ignore(x => x." + field.Name + ");");
        }
    }
}
