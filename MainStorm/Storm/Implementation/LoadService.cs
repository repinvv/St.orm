namespace Storm.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Storm.Interfaces;

    internal class LoadService : ILoadService
    {
        private readonly object[] fields;

        public LoadService(IStormContext context, int navPropsCount)
        {
            Context = context;
            fields = new object[navPropsCount];
        }

        public IStormContext Context { get; }

        private ILookup<TIndex, TField> GetLookup<TField, TQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TQuery>> query,
            Func<TField, TIndex> indexLambda)
        {
            if (fields[navPropIndex] != null)
            {
                return fields[navPropIndex] as Lookup<TIndex, TField>;
            }

            var repo = Context.GetDalRepository<TField, TQuery>();
            var items = repo.Materialize(query(), new LoadService(Context, repo.NavPropsCount()))
                            .ToLookup(indexLambda);
            fields[navPropIndex] = items;
            return items;
        }

        public List<TField> GetList<TField, TQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex key)
        {
            return key == null
                ? new List<TField>()
                : GetLookup(navPropIndex, query, indexLambda)[key].ToList();
        }

        public List<TField> GetList<TField, TQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex? key)
            where TIndex : struct
        {
            return key.HasValue
                ? GetLookup(navPropIndex, query, indexLambda)[key.Value].ToList()
                : new List<TField>();
        }

        public TField GetSingle<TField, TQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex key)
            where TField : class
        {
            return key == null
                ? null
                : GetLookup(navPropIndex, query, indexLambda)[key].FirstOrDefault();
        }

        public TField GetSingle<TField, TQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex? key)
            where TIndex : struct
            where TField : class
        {
            return key.HasValue ? GetLookup(navPropIndex, query, indexLambda)[key.Value].FirstOrDefault() : null;
        }
    }
}
