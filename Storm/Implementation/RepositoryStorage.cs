namespace St.Orm.Implementation
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Interfaces;
    using St.Orm.Interfaces.Internal;

    internal static class RepositoryStorage
    {
        private static readonly Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        public static IDalRepository<TDal> GetRepository<TDal>() where TDal : IDalEntity
        {
            object repo;
            if (!Repositories.TryGetValue(typeof(TDal), out repo))
            {
                Repositories[typeof(TDal)] = repo = typeof(TDal).IsValueType ? default(TDal) : Activator.CreateInstance(typeof(TDal));
            }

            return repo as IDalRepository<TDal>;
        }
    }
}
