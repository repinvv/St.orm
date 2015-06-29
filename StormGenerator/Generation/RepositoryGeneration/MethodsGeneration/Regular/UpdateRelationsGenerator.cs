namespace StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Regular
{
    using StormGenerator.Generation.RepositoryGeneration.MethodsGeneration.Relations;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class UpdateRelationsGenerator : IMethodGenerator
    {
        private readonly RelationsGeneratorFactory relationsGeneratorFactory;

        public UpdateRelationsGenerator(RelationsGeneratorFactory relationsGeneratorFactory)
        {
            this.relationsGeneratorFactory = relationsGeneratorFactory;
        }

        public void GenerateSignature(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public void UpdateRelations(" + model.Name + " entity, " + model.Name
                                       + " existing, ISavesCollector saves)");
        }

        public void GenerateMethod(Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("var populated = (entity as ICloneable<Policy>).GetPopulated();");
            var fields = model.RelationFields.Active();
            for (int index = 0; index < fields.Count; index++)
            {
                var field = fields[index];
                var generator = relationsGeneratorFactory.GetRelationsGenerator(field);
                if (generator is EmptyRelationsGenerator)
                {
                    continue;
                }

                stringGenerator.AppendLine("if(populated[" + index + "])");
                stringGenerator.Braces(() => generator.GenerateUpdateRelation(field, stringGenerator));
                stringGenerator.AppendLine();
            }

            stringGenerator.AppendLine("extension.ExtendSaveRelations(entity, saves);");
        }
    }
}
