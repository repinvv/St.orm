namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class OneToManyLazyGenerator
    {
        private readonly JoinGenerator joinGenerator;

        public OneToManyLazyGenerator(JoinGenerator joinGenerator)
        {
            this.joinGenerator = joinGenerator;
        }

        public void GenerateLazyGet(OneToManyField field, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("Func<IQueryable<" + field.FieldModel.Parent.Name + ">> query = () =>");
            stringGenerator.Braces(() => GenerateJoin(field, model, stringGenerator), true);
        }

        private void GenerateJoin(OneToManyField field, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("return loadService.Context.Set<" + field.FieldModel.Parent.Name + ">()");
            stringGenerator.PushIndent();
            joinGenerator.GenerateJoin(field.FarEndFields, field.NearEndFields, stringGenerator, true);
            stringGenerator.PopIndent();
        }
    }
}
