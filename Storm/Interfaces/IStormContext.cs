namespace St.Orm.Interfaces
{
    using System.Linq;

    public interface IStormContext
    {
        IQueryable<T> DbSet<T>() where T : class;

        IDalRepositoryStorage Storage { get; }

        ////DbConnection Connection { get; }

        ////DbTransaction Transaction { get; }
    }
}
