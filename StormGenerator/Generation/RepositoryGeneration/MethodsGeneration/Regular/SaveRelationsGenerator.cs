namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class SaveRelationsGenerator : IMethodGenerator
    {
        private readonly RelationsGeneratorFactory relationsGeneratorFactory;

        public SaveRelationsGenerator(RelationsGeneratorFactory relationsGeneratorFactory)
        {
            this.relationsGeneratorFactory = relationsGeneratorFactory;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void SaveRelations(" + model.Name + " entity, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            foreach (var field in model.RelationFields.Active())
            {
                relationsGeneratorFactory.GetRelationsGenerator(field)
                    .GenerateSaveRelation(field, stringGenerator);
            }

            stringGenerator.AppendLine("extension.ExtendSaveRelations(entity, saves);");
        }
    }
}
