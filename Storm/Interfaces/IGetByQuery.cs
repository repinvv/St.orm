namespace St.Orm.Interfaces
{
    using System.Collections.Generic;
    using St.Orm.Parameters;

    /// <summary>
    /// Object implementing this interface will be returned by Storm "ByQuery" method
    /// </summary>
    /// <typeparam name="TDb"></typeparam>
    public interface IGetByQuery<TDb>
    {
        /// <summary>
        /// Allows to specify type of entities to convert a result of the query to.
        /// </summary>
        /// <typeparam name="TDal"></typeparam>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<TDal> Get<TDal>(IStormContext context, params LoadParameter[] parameters) where TDal : IDalEntity<TDb>;
    }
}
