namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Interfaces;

    internal class EntityStorage
    {
        private readonly Dictionary<Type, IEntityList> lists = new Dictionary<Type, IEntityList>();

        public void Add<TDal, TQuery>(TDal entity, Func<IDalRepository<TDal, TQuery>> func) where TDal : class
        {
            IEntityList list;
            if (!lists.TryGetValue(typeof(TDal), out list))
            {
                lists[typeof(TDal)] = list = new EntityList<TDal, TQuery>(func());
            }

            list.Add(entity);
        }

        public IEnumerable<IEntityList> GetLists()
        {
            return lists.Values;
        }
    }
}
