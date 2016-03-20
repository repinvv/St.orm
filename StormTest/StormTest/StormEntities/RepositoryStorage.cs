namespace StormTest.StormEntities
{
    using System;
    using System.Collections.Generic;
    using Storm.Interfaces;
    using StormTest.Entities;

    internal static class RepositoryStorage
    {
        private static readonly Dictionary<Type, object> repositories
            = new Dictionary<Type, object>
              {
                  { typeof(company), new CompanyRepository() },
                  { typeof(department), new DepartmentRepository() }
              };

        public static IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>()
        {
            return repositories[typeof(TDal)] as IDalRepository<TDal, TQuery>;
        }
    }
}
