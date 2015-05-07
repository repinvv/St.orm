namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;
    using St.Orm.Interfaces.Internal;

    internal class LoadService<TDal> : ILoadService<TDal>
        where TDal : IDalEntity
    {
        private readonly ICustomContext context;
        private readonly object[] fields;
        private readonly Dictionary<Type, object> inheritedServices = new Dictionary<Type, object>();

        public LoadService(Dictionary<object, object> parametersDictionary, ICustomContext context)
        {
            Parameters = parametersDictionary;
            this.context = context;
            fields = new object[RepositoryStorage.GetRepository<TDal>().RelationPropertiesCount];
        }

        public Dictionary<object, object> Parameters { get; private set; }

        public ICustomContext Context { get { return context; } }

        public ILoadService<TInherited> ForType<TInherited>() where TInherited : IDalEntity
        {
            object output;
            if (inheritedServices.TryGetValue(typeof(TInherited), out output))
            {
                return output as ILoadService<TInherited>;
            }

            var inherited = new LoadService<TInherited>(Parameters, context);
            inheritedServices.Add(typeof(TInherited), inherited);
            return inherited;
        }

        private Dictionary<TIndex, List<TField>> GetItemsDictionary<TField, TIndex>(int propertyIndex, IQueryable<object> query, Func<TField, TIndex> indexLambda)
            where TField : IDalEntity
        {
            if (fields[propertyIndex] != null)
            {
                return fields[propertyIndex] as Dictionary<TIndex, List<TField>>;
            }

            var items = new Dictionary<TIndex, List<TField>>();
            fields[propertyIndex] = items;
            var materialized = RepositoryStorage
                .GetRepository<TField>()
                .Materialize(query, new LoadService<TField>(Parameters, context));
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

        public List<TField> GetProperty<TField, TIndex>(int propertyIndex, IQueryable<object> query, Func<TField, TIndex> indexLambda, TIndex key)
            where TField : IDalEntity
            where TIndex : class
        {
            return key == null ? new List<TField>() : GetItemsDictionary(propertyIndex, query, indexLambda)[key];
        }

        public List<TField> GetProperty<TField, TIndex>(int propertyIndex, IQueryable<object> query, Func<TField, TIndex> indexLambda, TIndex? key)
            where TField : IDalEntity
            where TIndex : struct
        {
            return key.HasValue ? GetItemsDictionary(propertyIndex, query, indexLambda)[key.Value] : new List<TField>();
        }
    }
}
