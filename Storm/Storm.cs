namespace St.Orm
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Implementation;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    public static class Storm
    {
        public static IGetByQuery<TDb> ByQuery<TDb>(IQueryable<TDb> query)
        {
            return new GetByQuery<TDb>(query);
        }

        public static List<TDal> GetEntities<TDal, TDb>(IQueryable<TDb> query, IStormContext context, params LoadParameter[] parameters) where TDal : IDalEntity<TDb>
        {
            return StormGetImplementation.GetEntities<TDal>(query, context, parameters);
        }
    }
}
