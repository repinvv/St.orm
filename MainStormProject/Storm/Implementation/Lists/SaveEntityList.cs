namespace St.Orm.Implementation.Lists
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Interfaces;

    internal class SaveEntityList<TDal, TQuery> : IEntityList where TDal : class
    {
        private readonly IDalRepository<TDal, TQuery> repo;
        private readonly List<TDal> list = new List<TDal>();

        public SaveEntityList(IDalRepository<TDal, TQuery> repo)
        {
            this.repo = repo;
        }

        public void Add(TDal entity)
        {
            list.Add(entity);
        }

        public void PersistenceAction(IStormContext context)
        {
            repo.Insert(context, list);
        }

        public void RelationAction(SavesCollector saves)
        {
            list.ForEach(x => repo.SaveRelations(x, saves));
        }
    }
}
