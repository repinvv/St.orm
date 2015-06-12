namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;

    internal class LoadService<TDal> : ILoadService<TDal>
    {
        private readonly IStormContext context;
        private readonly object[] fields;
        private readonly Dictionary<Type, object> inheritedServices = new Dictionary<Type, object>();

        public LoadService(Dictionary<object, object> parametersDictionary, IStormContext context, int relationPropertiesCount)
        {
            Parameters = parametersDictionary;
            this.context = context;
            fields = new object[relationPropertiesCount];
        }

        public Dictionary<object, object> Parameters { get; private set; }

        public IStormContext Context { get { return context; } }

        private Dictionary<TIndex, List<TField>> GetItemsDictionary<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda)
        {
            if (fields[propertyIndex] != null)
            {
                return fields[propertyIndex] as Dictionary<TIndex, List<TField>>;
            }
            
            var repo = context.GetDalRepository<TField, TQuery>();
            var materialized = repo.Materialize(query(), new LoadService<TField>(Parameters, context, repo.RelationPropertiesCount()));
            var items = CreateDictionary(indexLambda, materialized);
            fields[propertyIndex] = items;
            return items;
        }

        private Dictionary<TIndex, List<TField>> CreateDictionary<TField, TIndex>(Func<TField, TIndex> indexLambda, List<TField> materialized)
        {
            var items = new Dictionary<TIndex, List<TField>>();
            foreach (var entity in materialized)
            {
                List<TField> list;
                var itemKey = indexLambda(entity);
                if (!items.TryGetValue(itemKey, out list))
                {
                    list = new List<TField>();
                    items.Add(itemKey, list);
                }

                list.Add(entity);
            }

            return items;
        }

        public List<TField> GetProperty<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda, TIndex key)
        {
            return key == null ? new List<TField>() : GetItemsDictionary(propertyIndex, query, indexLambda)[key];
        }

        public List<TField> GetProperty<TField, TQuery, TIndex>(int propertyIndex, Func<IQueryable<TQuery>> query, Func<TField, TIndex> indexLambda, TIndex? key)
            where TIndex : struct
        {
            return key.HasValue ? GetItemsDictionary(propertyIndex, query, indexLambda)[key.Value] : new List<TField>();
        }
    }
}
