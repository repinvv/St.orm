namespace Storm.Interfaces
{
    using System.Data.Common;
    using System.Linq;

    public interface IStormContext
    {
        IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>();

        IQueryable<TDal> Set<TDal>() where TDal : class;

        DbConnection Connection { get; }

        DbTransaction Transaction { get; }
    }
}
