namespace St.Orm.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ILoadService<TDal>
    {
        Dictionary<object, object> Parameters { get; }

        IStormContext Context { get; }

        List<TField> GetProperty<TField, TIndex>(int propertyIndex, Func<IQueryable> query, Func<TField, TIndex> indexLambda, TIndex key);

        List<TField> GetProperty<TField, TIndex>(int propertyIndex, Func<IQueryable> query, Func<TField, TIndex> indexLambda, TIndex? key)
            where TIndex : struct;
    }
}
