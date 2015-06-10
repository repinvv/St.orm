namespace St.Orm.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IDalRepository<TDal>
    {
        int RelationPropertiesCount();

        TDal Clone(TDal item);

        List<TDal> Materialize(IQueryable query, ILoadService<TDal> loadService);
    }
}
