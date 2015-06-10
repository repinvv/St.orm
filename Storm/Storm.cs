namespace St.Orm
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Implementation;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    public static class Storm
    {
        public static List<TDal> GetEntities<TDal>(IQueryable<TDal> query, IStormContext context, params LoadParameter[] parameters)
        {
            return StormGetImplementation.GetEntities(query, context, parameters);
        }
    }
}
