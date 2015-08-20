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
        public string GetName()
        {
            return "Storm.Commands";
        }

        public void GenerateContent(List<Model> models, IStringGenerator stringGenerator)
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

        public IList<T> Get<T>(IQueryable<T> query, params LoadParameter[] parameters)
        {
            return Storm.Get(query, context, parameters);
        }

        public T GetById<T>(object id, params LoadParameter[] parameters) where T : IHaveId
        {
            return Storm.GetById<T>(id, context, parameters);
        }

        public void Save<T>(IList<T> entities)
        {
            Storm.Save(entities, context);
        }

        public void Save<T>(IList<T> entities, IList<T> existing)
        {
            Storm.Save(entities, existing, context);
        }

        public void Save<T>(T entity)
        {
            Storm.Save(entity, context);
        }

        public void Save<T>(T entity, T existing)
        {
            Storm.Save(entity, existing, context);
        }
    }");
        }
    }
}
