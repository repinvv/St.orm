namespace St.Orm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;
    using St.Orm.Interfaces.Internal;
    using St.Orm.Parameters;

    internal static class StormGetImplementation
    {
        public static List<TDal> GetEntities<TDal>(IQueryable query, ICustomContext context, LoadParameter[] parameters) where TDal : IDalEntity
        {
            var parametersDictionary = parameters.ToDictionary(x => x.Key, x => x.Value);
            var loadService = new LoadService<TDal>(parametersDictionary, context);
            var repo = RepositoryStorage.GetRepository<TDal>();
            var items = repo.Materialize(query, loadService);
            var result = new List<TDal>(items.Count);
            result.AddRange(items.Select(x => (x as IAmLazy<TDal>).Clone()));
            return result;
        }
    }
}
