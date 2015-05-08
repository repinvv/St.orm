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
            var type = typeof(TDal);
            if (!Repositories.TryGetValue(type, out repo))
            {
                Repositories[type] = repo = Activator.CreateInstance(type);
            }

            return repo as IDalRepository<TDal>;
        }
    }
}
