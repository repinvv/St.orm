namespace St.Orm.Interfaces.Internal
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IDalRepository<TDal> where TDal : IDalEntity
    {
        int RelationPropertiesCount { get; }

        List<TDal> Materialize(IQueryable query, ILoadService<TDal> loadService);
    }
}
