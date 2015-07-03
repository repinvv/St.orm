﻿namespace St.Orm.Interfaces
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;

    public interface IStormContext
    {
        IQueryable<T> Set<T>() where T : class;

        IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>();

        DbConnection Connection { get; }

        DbTransaction Transaction { get; }
    }
}
