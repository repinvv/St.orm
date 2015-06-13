//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject
{
    using System;
    using System.Collections.Generic;
    using St.Orm.Interfaces;
    
    internal static class DalRepositoryStorage
    {
        private static readonly Dictionary<Type, object> repositories 
            = new Dictionary<Type, object>
                {
                    { typeof(EligibilityGroup), new EligibilityGroupDalRepository() },
                    { typeof(Currency), new CurrencyDalRepository() },
                    { typeof(Country), new CountryDalRepository() },
                    { typeof(Policy), new PolicyDalRepository() },
                    { typeof(Tax), new TaxDalRepository() },
                    { typeof(Coverage), new CoverageDalRepository() },
                    { typeof(CoverageDepartment), new CoverageDepartmentDalRepository() },
                    { typeof(CoverageEligibilityGroup), new CoverageEligibilityGroupDalRepository() },
                    { typeof(Premium), new PremiumDalRepository() },
                    { typeof(Covered), new CoveredDalRepository() },
                    { typeof(Comment), new CommentDalRepository() },
                    { typeof(Emp), new EmpDalRepository() },
                    { typeof(EmpToDependent), new EmpToDependentDalRepository() },
                    { typeof(Calculation), new CalculationDalRepository() },
                    { typeof(CalculationDetails), new CalculationDetailsDalRepository() },
                    { typeof(Department), new DepartmentDalRepository() },
                };
    
        public static IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>()
        {
            return repositories[typeof(TDal)] as IDalRepository<TDal, TQuery>;
        }
    }
}
