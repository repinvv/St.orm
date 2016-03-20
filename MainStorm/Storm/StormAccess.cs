namespace Storm
{
    using System.Collections.Generic;
    using System.Linq;
    using Storm.Implementation;
    using Storm.Interfaces;
    using Storm.Parameters;

    public static class StormAccess
    {
        public static List<TDal> Get<TDal>(this IStormContext context, IQueryable<TDal> query, params Parameter[] parameters)
        {
            return StormGetImplementation.Get(context, query);
        }
    }
}
