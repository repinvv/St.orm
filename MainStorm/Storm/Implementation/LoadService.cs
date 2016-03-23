﻿namespace Storm.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Storm.Interfaces;

    internal class LoadService<TQuery> : ILoadService<TQuery>
    {
        private readonly object[] fields;

        public LoadService(IStormContext context, int navPropsCount, IQueryable<TQuery> query)
        {
            Context = context;
            Query = query;
            fields = new object[navPropsCount];
        }

        public IStormContext Context { get; }

        public IQueryable<TQuery> Query { get; set; }

        private ILookup<TIndex, TField> GetLookup<TField, TFieldQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TFieldQuery>> query,
            Func<TField, TIndex> indexLambda)
        {
            if (fields[navPropIndex] != null)
            {
                return fields[navPropIndex] as Lookup<TIndex, TField>;
            }

            var repo = Context.GetDalRepository<TField, TFieldQuery>();
            var items = repo.Materialize(new LoadService<TFieldQuery>(Context, repo.NavPropsCount(), query()))
                            .ToLookup(indexLambda);
            fields[navPropIndex] = items;
            return items;
        }

        public List<TField> GetList<TField, TFieldQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TFieldQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex key)
        {
            return key == null
                ? new List<TField>()
                : GetLookup(navPropIndex, query, indexLambda)[key].ToList();
        }

        public List<TField> GetList<TField, TFieldQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TFieldQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex? key)
            where TIndex : struct
        {
            return key.HasValue
                ? GetLookup(navPropIndex, query, indexLambda)[key.Value].ToList()
                : new List<TField>();
        }

        public TField GetSingle<TField, TFieldQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TFieldQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex key)
            where TField : class
        {
            return key == null
                ? null
                : GetLookup(navPropIndex, query, indexLambda)[key].FirstOrDefault();
        }

        public TField GetSingle<TField, TFieldQuery, TIndex>(int navPropIndex,
            Func<IQueryable<TFieldQuery>> query,
            Func<TField, TIndex> indexLambda,
            TIndex? key)
            where TIndex : struct
            where TField : class
        {
            return key.HasValue ? GetLookup(navPropIndex, query, indexLambda)[key.Value].FirstOrDefault() : null;
        }
    }
}
