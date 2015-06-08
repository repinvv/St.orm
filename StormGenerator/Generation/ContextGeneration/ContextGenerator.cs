namespace StormGenerator.Generation.ContextGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ContextGenerator
    {
        private readonly IStringGenerator stringGenerator;
        private readonly UsingsGenerator usingsGenerator;
        private readonly NameCreator nameCreator;
        private readonly InitializersGenerator initializersGenerator;

        public ContextGenerator(IStringGenerator stringGenerator,
            UsingsGenerator usingsGenerator,
            NameCreator nameCreator,
            InitializersGenerator initializersGenerator)
        {
            this.stringGenerator = stringGenerator;
            this.usingsGenerator = usingsGenerator;
            this.nameCreator = nameCreator;
            this.initializersGenerator = initializersGenerator;
        }

        public GeneratedFile GenerateContext(List<Model> models, string contextName, string outputNamespace)
        {
            return new GeneratedFile
                   {
                       Name = contextName,
                       Content = GenerateContent(models, contextName, outputNamespace)
                   };
        }

        private string GenerateContent(List<Model> models, string contextName, string outputNamespace)
        {
            stringGenerator.AppendLine("namespace " + outputNamespace);
            stringGenerator.Braces(() => GenerateContextClass(models, contextName));
            return stringGenerator.ToString();
        }

        private void GenerateContextClass(List<Model> models, string contextName)
        {
            usingsGenerator.GenerateUsings(stringGenerator, GenerationConstants.ModelGeneration.ContextUsings);
            stringGenerator.AppendLine();
            stringGenerator.AppendLine("public partial class " + contextName + " : DbContext");
            stringGenerator.Braces(() => GenerateClassContents(models, contextName));
        }

        private void GenerateClassContents(List<Model> models, string contextName)
        {
            stringGenerator.AppendLine("public " + contextName + "() : base(\"name=" + contextName + "\") { }");
            stringGenerator.AppendLine();
            foreach (var model in models.Where(x => !x.IsManyToManyLink))
            {
                stringGenerator.AppendLine("public virtual DbSet<" + model.Name + "> " + nameCreator.CreatePluralName(model.Name) + " { get; set; }");
                stringGenerator.AppendLine();
            }

            stringGenerator.AppendLine("protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            stringGenerator.Braces(() => GenerateInitializerCalls(models));
            initializersGenerator.GenerateInitializers(models, stringGenerator);
        }

        private void GenerateInitializerCalls(List<Model> models)
        {
            foreach (var model in models
                .Where(x => !x.IsManyToManyLink)
                .Where(x => x.RelationFields.Any()))
            {
                stringGenerator.AppendLine("Initialize" + model.Name + "Relations(modelBuilder);");
            }
        }
    }
}
