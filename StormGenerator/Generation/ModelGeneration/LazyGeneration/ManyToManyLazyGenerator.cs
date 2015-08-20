namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class ManyToManyLazyGenerator
    {
        private readonly JoinGenerator joinGenerator;

        public ManyToManyLazyGenerator(JoinGenerator joinGenerator)
        {
            this.joinGenerator = joinGenerator;
        }

        public void GenerateLazyGet(ManyToManyField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"Func<IQueryable<{field.MediatorModel.Parent.Name}>> query = () =>");
            stringGenerator.Braces(() => GenerateJoin(field, model, stringGenerator), true);
            joinGenerator.GenerateGetItems(field.MediatorModel, index, field.FarEndFields, field.NearEndFields, stringGenerator, false, field.IsList);
            stringGenerator.PushIndent();
            stringGenerator.AppendLine($".Select(x => x.{field.MediatorMtoField.Name})");
            stringGenerator.AppendLine(".ToList();");
            stringGenerator.PopIndent();
        }

        private void GenerateJoin(ManyToManyField field, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine($"return loadService.Context.Set<{field.MediatorModel.Parent.Name}>()");
            stringGenerator.PushIndent();
            joinGenerator.GenerateJoin(field.FarEndFields, field.NearEndFields, stringGenerator, true);
            stringGenerator.PopIndent();
        }
    }
}
