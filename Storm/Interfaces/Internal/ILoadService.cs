namespace St.Orm.Interfaces.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ILoadService<TDal> where TDal : IDalEntity
    {
        Dictionary<object, object> Parameters { get; }

        ICustomContext Context { get; }
            
        ILoadService<TInherited> ForType<TInherited>() where TInherited : IDalEntity;

        List<TField> GetProperty<TField, TIndex>(int propertyIndex, IQueryable<object> query, Func<TField, TIndex> indexLambda, TIndex key)
            where TField : IDalEntity
            where TIndex : class;

        List<TField> GetProperty<TField, TIndex>(int propertyIndex, IQueryable<object> query, Func<TField, TIndex> indexLambda, TIndex? key)
            where TField : IDalEntity
            where TIndex : struct;
    }
}
