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
    using System.Linq;
    using St.Orm.Interfaces;

    internal class CoverageEligibilityGroupDalRepository : IDalRepository<CoverageEligibilityGroup, CoverageEligibilityGroup>
    {
        private readonly IDalRepositoryExtension<CoverageEligibilityGroup> extension;

        public CalculationDalRepository(IDalRepositoryExtension<CoverageEligibilityGroup> extension)
        {
            this.extension = extension;
        }

        public int RelationPropertiesCount()
        {
            return extension.RelationsCount() ?? 1;
        }
    }
}
