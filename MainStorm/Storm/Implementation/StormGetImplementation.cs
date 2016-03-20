﻿namespace Storm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using Storm.Interfaces;

    internal static class StormGetImplementation
    {
        public static List<TDal> Get<TDal>(IStormContext context, IQueryable<TDal> query)
        {
            var repo = context.GetDalRepository<TDal, TDal>();
            var items = repo.Materialize(query, new LoadService(context, repo.NavPropsCount()));
            var result = new List<TDal>(items.Count);
            result.AddRange(items.Select(repo.Clone));
            return result;
        }
    }
}
