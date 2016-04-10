namespace Storm.Interfaces
{
    using System.Collections.Generic;

    public interface IDalRepository<TDal, TQuery>
    {
        List<TDal> Materialize(ILoadService<TQuery> loadService);

        /// <summary>
        /// returns total count of navigation properties in TDal entity
        /// </summary>
        /// <returns></returns>
        int RelationsCount();

        TDal Clone(TDal item);
    }
}
