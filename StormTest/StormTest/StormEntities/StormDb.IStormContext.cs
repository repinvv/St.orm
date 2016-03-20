namespace StormTest.StormEntities
{
    using System.Data.Common;
    using System.Linq;
    using Storm.Interfaces;
    public partial class StormDb : IStormContext
    {
        public IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>()
        {
            return RepositoryStorage.GetDalRepository<TDal, TQuery>();
        }

        public IQueryable<TDal> Set<TDal>() where TDal : class
        {
            return GetTable<TDal>();
        }

        DbConnection IStormContext.Connection { get { return Connection as DbConnection; } }
        DbTransaction IStormContext.Transaction { get { return Transaction as DbTransaction; } }
    }
}
