namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using System.Linq;
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class DeleteRelationsGenerator : IMethodGenerator
    {
        private readonly RelationsGeneratorFactory relationsGeneratorFactory;

        public DeleteRelationsGenerator(RelationsGeneratorFactory relationsGeneratorFactory)
        {
            this.relationsGeneratorFactory = relationsGeneratorFactory;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"private void DeleteRelations({model.Name} entity, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            foreach (var field in model.RelationFields.Active(x => !x.Cascade))
            {
                relationsGeneratorFactory.GetRelationsGenerator(field)
                                         .GenerateDeleteRelation(field, stringGenerator);
            }
        }
    }
}
