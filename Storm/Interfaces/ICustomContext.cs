namespace St.Orm.Interfaces
{
    using System.Data.Common;
    using System.Linq;

    public interface ICustomContext
    {
        IQueryable<T> Set<T>() where T : class;

        DbConnection Connection { get; }

        DbTransaction Transaction { get; }
    }
}
