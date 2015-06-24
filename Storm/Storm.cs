namespace St.Orm
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Implementation;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    public static class Storm
    {
        public static List<TDal> Get<TDal>(IQueryable<TDal> query, IStormContext context, params LoadParameter[] parameters)
        {
            return StormGetImplementation.Get(query, context, parameters);
        }

        public static TDal GetById<TDal>(object id, IStormContext context, params LoadParameter[] parameters)
        {
            var query = context.GetDalRepository<TDal, TDal>().GetByIdQuery(id, context);
            return StormGetImplementation.Get(query, context, parameters).SingleOrDefault();
        }
    }
}
