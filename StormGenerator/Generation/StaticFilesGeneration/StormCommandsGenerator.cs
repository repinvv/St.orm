namespace StormGenerator.Generation.StaticFilesGeneration
{
    using System.Collections.Generic;
    using St.Orm;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;
    using StormGenerator.Infrastructure.StringGenerator;
    using StormGenerator.Models.Pregen;

    internal class StormCommandsGenerator : IStaticFileGenerator
    {
        public string GetName(Options options)
        {
            return "Storm.Commands";
        }

        public void GenerateContent(List<Model> models, Options options, IStringGenerator stringGenerator)
        {
            stringGenerator.AppendLine(@"using System.Collections.Generic;
    using System.Linq;
    using " + typeof(Storm).Namespace + @";    
    using " + typeof(IStormContext).Namespace + @";    
    using " + typeof(LoadParameter).Namespace + @";

    public class StormCommands
    {
        private readonly IStormContext context;

        public StormCommands(IStormContext context)
        {
            this.context = context;
        }

        public ICollection<T> Get<T>(IQueryable<T> query, params LoadParameter[] parameters)
        {
            return Storm.GetEntities(query, context, parameters);
        }
    }");
        }
    }
}
