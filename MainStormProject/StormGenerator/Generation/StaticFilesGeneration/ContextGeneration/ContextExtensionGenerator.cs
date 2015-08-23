namespace StormGenerator.Generation.StaticFilesGeneration.ContextGeneration
{
    using System.Collections.Generic;
    using St.Orm.Interfaces;
    using StormGenerator.Common;
    using StormGenerator.Infrastructure;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class ContextExtensionGenerator : IStaticFileGenerator
    {
        private readonly OptionsService options;

        public ContextExtensionGenerator(OptionsService options)
        {
            this.options = options;
        }

        public string GetName()
        {
            return "Storm." + options.Options.ContextName + GenerationConstants.ModelGeneration.ContextExtensionSuffix;
        }

        public void GenerateContent(List<Model> models, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System.Data.Common;
    using System.Linq;
    using " + typeof(IStormContext).Namespace + @";

    public partial class " + options.Options.ContextName + @" : IStormContext
    {
        private StormCommands stormCommands;

        IQueryable<T> IStormContext.Set<T>()
        {
            return Set<T>();
        }

        IDalRepository<TDal, TQuery> IStormContext.GetDalRepository<TDal, TQuery>()
        {
            return DalRepositoryStorage.GetDalRepository<TDal, TQuery>();
        }

        public StormCommands Storm
        {
            get
            {
                return stormCommands = stormCommands ?? new StormCommands(this);
            }
        }

        public DbConnection Connection { get { return Database.Connection; } }

        public DbTransaction Transaction
        {
            get
            {
                return Database.CurrentTransaction != null ? Database.CurrentTransaction.UnderlyingTransaction : null;
            }
        }
    }");
        }
    }
}
