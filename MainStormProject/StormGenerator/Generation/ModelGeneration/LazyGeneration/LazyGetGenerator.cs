namespace StormGenerator.Generation.ModelGeneration.LazyGeneration
{
    using StormGenerator.Generation.RepositoryGeneration.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;
    using StormGenerator.Models.Pregen.Relation;

    internal class LazyGetGenerator
    {
        private readonly OneToManyLazyGenerator oneToManyLazyGenerator;
        private readonly ManyToOneLazyGenerator manyToOneLazyGenerator;
        private readonly ManyToManyLazyGenerator manyToManyLazyGenerator;
        private readonly Generics generics;

        public LazyGetGenerator(OneToManyLazyGenerator oneToManyLazyGenerator,
            ManyToOneLazyGenerator manyToOneLazyGenerator,
            ManyToManyLazyGenerator manyToManyLazyGenerator,
            Generics generics)
        {
            this.oneToManyLazyGenerator = oneToManyLazyGenerator;
            this.manyToOneLazyGenerator = manyToOneLazyGenerator;
            this.manyToManyLazyGenerator = manyToManyLazyGenerator;
            this.generics = generics;
        }

        public void GenerateLazyGet(RelationField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if(populated[" + index + "] || loadService == null)");
            stringGenerator.Braces("return field" + index + ";");
            stringGenerator.AppendLine();

            var otm = field as OneToManyField;
            if (otm != null)
            {
                oneToManyLazyGenerator.GenerateLazyGet(otm, index, model, stringGenerator);
            }

            var mto = field as ManyToOneField;
            if (mto != null)
            {
                manyToOneLazyGenerator.GenerateLazyGet(mto, index, model, stringGenerator);
            }

            var mtm = field as ManyToManyField;
            if (mtm != null)
            {
                manyToManyLazyGenerator.GenerateLazyGet(mtm, index, model, stringGenerator);
            }

            if (field.IsList)
            {
                GenerateList(field, index, model, stringGenerator);
            }
            else
            {
                GenerateSingle(field, index, model, stringGenerator);
            }

            stringGenerator.AppendLine();
            stringGenerator.AppendLine($"populated[{index}] = true;");
            stringGenerator.AppendLine($"return field{index};");
        }

        private void GenerateList(RelationField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if (clonedFrom == null)");
            stringGenerator.Braces($"field{index} = items;");
            stringGenerator.AppendLine("else");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine($"clonedFrom.{field.Name} = items;");
                stringGenerator.AppendLine($"field{index} = new List<{field.FieldModel.Name}>(items.Count);");
                stringGenerator.AppendLine($"var repo = loadService.Context.GetDalRepository{generics.Line(field.FieldModel)}();");
                stringGenerator.AppendLine("foreach(var item in items)");
                stringGenerator.Braces($"field{index}.Add(repo.Clone(item));");
            });
        }

        private void GenerateSingle(RelationField field, int index, Model model, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("if (clonedFrom == null)");
            stringGenerator.Braces($"field{index} = item;");
            stringGenerator.AppendLine("else");
            stringGenerator.Braces(() =>
            {
                stringGenerator.AppendLine($"clonedFrom.{field.Name} = item;");
                stringGenerator.AppendLine($"field{index} = loadService.Context.GetDalRepository{generics.Line(field.FieldModel)}().Clone(item);");
            });
        }
    }
}
