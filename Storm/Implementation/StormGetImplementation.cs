namespace St.Orm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    internal static class StormGetImplementation
    {
        public static List<TDal> GetEntities<TDal, TDb>(IQueryable<TDb> query, ICustomContext context, LoadParameter[] parameters) where TDal : IDalEntity<TDb>
        {
            return new List<TDal>();
        }
    }
}
