namespace St.Orm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    internal static class StormGetImplementation
    {
        public static List<TDal> GetEntities<TDal>(IQueryable<TDal> query, IStormContext context, LoadParameter[] parameters)
        {
            var parametersDictionary = parameters.ToDictionary(x => x.Key, x => x.Value);
            var repo = context.Storage.GetDalRepository<TDal>();
            var loadService = new LoadService<TDal>(parametersDictionary, context, repo.RelationPropertiesCount());
            var items = repo.Materialize(query, loadService);
            var result = new List<TDal>(items.Count);
            foreach (var item in items)
            {
                result.Add(repo.Clone(item));
            }

            return result;
        }
    }
}
