namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Implementation.Lists;
    using St.Orm.Interfaces;

    internal class SavesCollector : ISavesCollector
    {
        private Dictionary<Type, IEntityList> saves = new Dictionary<Type, IEntityList>();

        public SavesCollector(IStormContext context)
        {
            Context = context;
        }

        public IStormContext Context { get; private set; }

        public void Save<TDal, TQuery>(TDal entity) where TDal : class
        {
            var save = Get(saves, typeof(TDal), () => new SaveEntityList<TDal, TQuery>(Context.GetDalRepository<TDal, TQuery>()));
            save.Add(entity);
        }

        public void Update<TDal, TQuery>(TDal entity, TDal existing)
        { }

        public void NoUpdate<TDal, TQuery>(TDal entity, TDal existing)
        { }

        public void Delete<TDal, TQuery>(TDal entity)
        { }

        public void Commit()
        {
            foreach (var group in saves.Values)
            {
                group.PersistenceAction(Context);
            }

            var nextTierCollector = new SavesCollector(Context);

            foreach (var group in saves.Values)
            {
                group.RelationAction(nextTierCollector);
            }
        }

        private T Get<T>(Dictionary<Type, IEntityList> dict, Type type, Func<T> create) where T : class, IEntityList
        {
            IEntityList list;
            if (!saves.TryGetValue(type, out list))
            {
                var save = create();
                saves[type] = save;
                return save;
            }

            return list as T;
        }
    }
}
