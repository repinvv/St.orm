namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Interfaces;

    internal class EntityList<TDal, TQuery> : IEntityList
        where TDal : class
    {
        private readonly IDalRepository<TDal, TQuery> repo;
        private readonly List<TDal> list = new List<TDal>();

        public EntityList(IDalRepository<TDal, TQuery> repo)
        {
            this.repo = repo;
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

        public IAdoOperation StartInsert(int count)
        {
            return repo.StartInsert();
        }
    }
}
