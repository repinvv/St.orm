namespace St.Orm.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public interface IDalRepository<TDal, TQuery> : ISaveRepository
    {
        int RelationsCount();

        void SetExtension(IDalRepositoryExtension<TDal> extension);

        TDal Clone(TDal item);

        TDal Create(IDataReader reader, IQueryable<TQuery> query, ILoadService loadService);

        List<TDal> Materialize(IQueryable<TQuery> query, ILoadService loadService);

        IQueryable<TQuery> GetByIdQuery(object id, IStormContext context);

        void Save(TDal entity, ISavesCollector saves);

        void Update(TDal entity, TDal existing, ISavesCollector saves);

        void Delete(TDal entity, ISavesCollector saves);

        void SaveRelations(TDal entity, ISavesCollector saves);

        void UpdateRelations(TDal entity, TDal existing, ISavesCollector saves);

        bool EntityChanged(TDal entity, TDal existing);
    }
}
