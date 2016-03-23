namespace Storm.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IDalRepository<TDal, TQuery>
    {
        List<TDal> Materialize(ILoadService<TQuery> loadService);

        /// <summary>
        /// returns total count of navigation properties in TDal entity
        /// </summary>
        /// <returns></returns>
        int NavPropsCount();

        TDal Clone(TDal item);
    }
}
