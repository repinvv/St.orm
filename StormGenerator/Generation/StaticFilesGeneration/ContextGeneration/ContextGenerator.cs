namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ContextGenerator : IStaticFileGenerator
    {
        private readonly UsingsGenerator usingsGenerator;
        private readonly NameCreator nameCreator;
        private readonly InitializersGenerator initializersGenerator;
        private readonly OptionsService options;

        public ContextGenerator(UsingsGenerator usingsGenerator,
            NameCreator nameCreator,
            InitializersGenerator initializersGenerator,
            OptionsService options)
        {
            this.usingsGenerator = usingsGenerator;
            this.nameCreator = nameCreator;
            this.initializersGenerator = initializersGenerator;
            this.options = options;
        }

        public string GetName()
        {
            return "Storm." + options.Options.ContextName;
        }

        public void GenerateContent(List<Model> models, IStringGenerator stringGenerator)
        {
            usingsGenerator.GenerateUsings(stringGenerator, GenerationConstants.ModelGeneration.ContextUsings);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public partial class " + options.Options.ContextName + " : DbContext");
            stringGenerator.Braces(() => GenerateClassContents(models, options.Options.ContextName, stringGenerator));
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
