namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class LazyGetGenerator
    {
        private readonly OneToManyLazyGenerator oneToManyLazyGenerator;
        private readonly ManyToOneLazyGenerator manyToOneLazyGenerator;
        private readonly ManyToManyLazyGenerator manyToManyLazyGenerator;

        public LazyGetGenerator(OneToManyLazyGenerator oneToManyLazyGenerator,
            ManyToOneLazyGenerator manyToOneLazyGenerator,
            ManyToManyLazyGenerator manyToManyLazyGenerator)
        {
            this.oneToManyLazyGenerator = oneToManyLazyGenerator;
            this.manyToOneLazyGenerator = manyToOneLazyGenerator;
            this.manyToManyLazyGenerator = manyToManyLazyGenerator;
        }

        public void GenerateLazyGet(RelationField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if(populated[" + index + "])");
            stringGenerator.Braces(() => stringGenerator.AppendLine("return field" + index + ";"));
            stringGenerator.AppendLine();

            var otm = field as OneToManyField;
            if (otm != null)
            {
                oneToManyLazyGenerator.GenerateLazyGet(otm, model, stringGenerator);
            }

            var mto = field as ManyToOneField;
            if (mto != null)
            {
                manyToOneLazyGenerator.GenerateLazyGet(mto, model, stringGenerator);
            }

            var mtm = field as ManyToManyField;
            if (mtm != null)
            {
                manyToManyLazyGenerator.GenerateLazyGet(mtm, model, stringGenerator);
            }

            stringGenerator.AppendLine("populated[" + index + "] = true;");
            stringGenerator.AppendLine("return field" + index + ";");
        }
    }
}
