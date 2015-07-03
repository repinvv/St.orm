namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Interfaces;

    internal class EntityList<TDal, TQuery> : IEntityList
        where TDal : class
    {
        private readonly Func<IDalRepository<TDal, TQuery>> repoFunc;
        private readonly List<TDal> list = new List<TDal>();

        public EntityList(Func<IDalRepository<TDal, TQuery>> repoFunc)
        {
            this.repoFunc = repoFunc;
        }

        public void Add(object entity)
        {
            list.Add((TDal)entity); 
        }

        public Type QueryType
        {
            get
            {
                return typeof(TQuery);
            }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void Insert(IStormContext context)
        {
            repoFunc().Insert(context, list);
        }
    }
}
