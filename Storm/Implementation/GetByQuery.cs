namespace St.Orm.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using St.Orm.Interfaces;
    using St.Orm.Parameters;

    internal class GetByQuery<TDb> : IGetByQuery<TDb>
    {
        private readonly IQueryable<TDb> query;

        public GetByQuery(IQueryable<TDb> query)
        {
            this.query = query;
        }

        public List<TDal> Get<TDal>(ICustomContext context, params LoadParameter[] parameters) where TDal : IDalEntity<TDb>
        {
            return StormGetImplementation.GetEntities<TDal>(query, context, parameters);
        }
    }
}
