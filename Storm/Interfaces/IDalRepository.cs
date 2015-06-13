namespace St.Orm.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public interface IDalRepository<TDal, TQuery>
    {
        int RelationsCount();

        void SetExtension(IDalRepositoryExtension<TDal> extension);

        TDal Clone(TDal item);

        TDal Create(IDataReader reader, ILoadService loadService);

        List<TDal> Materialize(IQueryable<TQuery> query, ILoadService loadService);
    }
}
