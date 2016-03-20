namespace Storm.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;

    public interface IAdoRepository<TAdo, TQuery>
    {
        List<TAdo> Materialize(IQueryable<TQuery> query, 
            DbConnection connection,
            DbTransaction transaction,
            Func<TAdo> create);
    }
}
