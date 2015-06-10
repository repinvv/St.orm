namespace StormTestProject
{
    using System.Linq;
    using St.Orm.Interfaces;

    public partial class StormTestContext : IStormContext
    {
        public IQueryable<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public IDalRepositoryStorage Storage
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
