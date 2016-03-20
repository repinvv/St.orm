namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;

    internal class LoadService : ILoadService
    {
        private readonly IStormContext context;
        private readonly object[] fields;

        public LoadService(Dictionary<object, object> parametersDictionary, IStormContext context, int relationPropertiesCount)
        {
            Parameters = parametersDictionary;
            this.context = context;
            fields = new object[relationPropertiesCount];
        }

        public Dictionary<object, object> Parameters { get; }

        public IStormContext Context => context;

        private ILookup<TIndex, TField> GetLookup<TField, TQuery, TIndex>(int propertyIndex,
            Func<IQueryable<TQuery>> query,
            Func<TField, TIndex> indexLambda)
        {
            if (fields[propertyIndex] != null)
            {
                return fields[propertyIndex] as Lookup<TIndex, TField>;
            }

            var repo = context.GetDalRepository<TField, TQuery>();
            var items = repo.Materialize(query(), new LoadService(Parameters, context, repo.RelationsCount()))
                            .ToLookup(indexLambda);
            fields[propertyIndex] = items;
            return items;
        }

        // will need many2many test with lots of entities, most likely lookup will remain
        private IDictionary<TIndex, TField> GetDictionary<TField, TQuery, TIndex>(int propertyIndex, 
            Func<IQueryable<TQuery>> query, 
            Func<TField, TIndex> indexLambda)
        {
            if (fields[propertyIndex] != null)
            {
                return fields[propertyIndex] as IDictionary<TIndex, TField>;
            }

            var repo = context.GetDalRepository<TField, TQuery>();
            var materialized = repo.Materialize(query(), new LoadService(Parameters, context, repo.RelationsCount()));
            var dict = new Dictionary<TIndex, TField>();
            foreach (var item in materialized)
            {
                var key = indexLambda(item);
                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, item);
                }
            }

            fields[propertyIndex] = dict;
            return dict;
        }

        public List<TField> GetList<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda, TIndex key)
        {
            return key == null ? new List<TField>() : GetLookup(propertyIndex, query, indexLambda)[key].ToList();
        }

        public List<TField> GetList<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda, TIndex? key) where TIndex : struct
        {
            return key.HasValue ? GetLookup(propertyIndex, query, indexLambda)[key.Value].ToList() : new List<TField>();
        }

        public TField GetSingle<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda, TIndex key)
            where TField : class
        {
            return key == null ? null : GetLookup(propertyIndex, query, indexLambda)[key].FirstOrDefault();
        }

        public TField GetSingle<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda, TIndex? key)
            where TIndex : struct
            where TField : class
        {
            return key.HasValue ? GetLookup(propertyIndex, query, indexLambda)[key.Value].FirstOrDefault() : null;
        }
    }
}
