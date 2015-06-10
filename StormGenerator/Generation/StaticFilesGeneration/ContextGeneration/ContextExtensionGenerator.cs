namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using System.Collections.Generic;
    using St.Orm.Interfaces;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ContextExtensionGenerator : IStaticFileGenerator
    {
        public string GetName(Options options)
        {
            return options.ContextName + GenerationConstants.ModelGeneration.ContextExtensionSuffix;
        }

        public void GenerateContent(List<Model> models, Options options, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System.Linq;
    using " + typeof(IStormContext).Namespace + @";

    public partial class " + options.ContextName + @" : IStormContext
    {
        private IDalRepositoryStorage storage;

        public IQueryable<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public virtual IDalRepositoryStorage Storage
        {
            get
            {
                return storage = storage ?? new DalRepositoryStorage();
            }
        }
    }");
        }
    }
}
