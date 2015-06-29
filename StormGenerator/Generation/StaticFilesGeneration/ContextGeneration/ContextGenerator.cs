namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ContextGenerator : IStaticFileGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly NameCreator nameCreator;
        private readonly InitializersGenerator initializersGenerator;

        public ContextGenerator(UsingsGenerator usingsGenerator,
            NameCreator nameCreator,
            InitializersGenerator initializersGenerator)
        {
            this.usingsGenerator = usingsGenerator;
            this.nameCreator = nameCreator;
            this.initializersGenerator = initializersGenerator;
        }

        public string GetName(Options options)
        {
            return "Storm." + options.ContextName;
        }

        public void GenerateContent(List<Model> models, Options options, IStringGenerator stringGenerator)
        {
            usingsGenerator.GenerateUsings(stringGenerator, GenerationConstants.ModelGeneration.ContextUsings);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public partial class " + options.ContextName + " : DbContext");
            stringGenerator.Braces(() => GenerateClassContents(models, options.ContextName, stringGenerator));
        }

        private void GenerateClassContents(List<Model> models, string contextName, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine("public " + contextName + "() : base(\"name=" + contextName + "\") { }");
            stringGenerator.AppendLine();
            foreach (var model in models)
            {
                stringGenerator.AppendLine("public virtual DbSet<" + model.Name + "> " + nameCreator.CreatePluralName(model.Name) + " { get; set; }");
                stringGenerator.AppendLine();
            }

            stringGenerator.AppendLine("protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            stringGenerator.Braces(() => GenerateInitializerCalls(models, stringGenerator));
            initializersGenerator.GenerateInitializers(models, stringGenerator);
        }

        private void GenerateInitializerCalls(List<Model> models, IStringGenerator stringGenerator)
        {
            foreach (var model in models.Where(initializersGenerator.NeedsInitializer))
            {
                stringGenerator.AppendLine("Initialize" + model.Name + "Fields(modelBuilder);");
            }
        }
    }
}
