namespace St.Orm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    internal static class StormGetImplementation
    {
        public static List<TDal> Get<TDal>(IQueryable<TDal> query, IStormContext context, LoadParameter[] parameters)
        {
            var parametersDictionary = parameters.ToDictionary(x => x.Key, x => x.Value);
            var repo = context.GetDalRepository<TDal, TDal>();
            var items = repo.Materialize(query, new LoadService(parametersDictionary, context, repo.RelationsCount()));
            var result = new List<TDal>(items.Count);
            foreach (var item in items)
            {
                result.Add(repo.Clone(item));
            }

            return result;
        }
    }
}
