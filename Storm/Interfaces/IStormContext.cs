namespace St.Orm.Interfaces
{
    using System.Linq;

    public interface IStormContext
    {
        IQueryable<T> Set<T>() where T : class;

        IDalRepository<T> GetDalRepository<T>();

        ////DbConnection Connection { get; }

        ////DbTransaction Transaction { get; }
    }
}
