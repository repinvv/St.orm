//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormModel
{
    using System;
    using System.Collections.Generic;
    using St.Orm;
    using St.Orm.Interfaces;
    
    internal static class DalRepositoryStorage
    {
        private static readonly Dictionary<Type, object> repositories 
            = new Dictionary<Type, object>
                {
                    { typeof(Department), new DepartmentDalRepository() },
                    { typeof(Eligibility), new EligibilityDalRepository() },
                    { typeof(Currency), new CurrencyDalRepository() },
                    { typeof(Country), new CountryDalRepository() },
                    { typeof(Calculation), new CalculationDalRepository() },
                    { typeof(CalculationDetails), new CalculationDetailsDalRepository() },
                    { typeof(Policy), new PolicyDalRepository() },
                    { typeof(Tax), new TaxDalRepository() },
                    { typeof(Assignment), new AssignmentDalRepository() },
                    { typeof(AssignmentDepartment), new AssignmentDepartmentDalRepository() },
                    { typeof(AssignmentEligibility), new AssignmentEligibilityDalRepository() },
                    { typeof(Premium), new PremiumDalRepository() },
                    { typeof(Covered), new CoveredDalRepository() },
                    { typeof(Comment), new CommentDalRepository() },
                };
    
        public static IDalRepository<TDal, TQuery> GetDalRepository<TDal, TQuery>()
        {
            return repositories[typeof(TDal)] as IDalRepository<TDal, TQuery>;
        }
    }
}
