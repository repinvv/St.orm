namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using StormGenerator.Generation.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class OneToManyLazyGenerator
    {
        private readonly JoinGenerator joinGenerator;
        private readonly TypeService typeService;

        public OneToManyLazyGenerator(JoinGenerator joinGenerator, TypeService typeService)
        {
            this.joinGenerator = joinGenerator;
            this.typeService = typeService;
        }

        public void GenerateLazyGet(OneToManyField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("Func<IQueryable<" + field.FieldModel.Parent.Name + ">> query = () =>");
            stringGenerator.Braces(() => GenerateJoin(field, model, stringGenerator), true);
            joinGenerator.GenerateGetItems(field.FieldModel, index, field.FarEndFields, field.NearEndFields, stringGenerator, true, field.IsList);
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
