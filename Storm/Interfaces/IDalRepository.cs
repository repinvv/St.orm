namespace St.Orm.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IDalRepository<TDal, TQuery>
    {
        int RelationPropertiesCount();

        TDal Clone(TDal item);

        List<TDal> Materialize(IQueryable<TQuery> query, ILoadService<TDal> loadService);
    }
}
