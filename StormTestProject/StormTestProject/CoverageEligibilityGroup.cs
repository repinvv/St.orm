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
    using St.Orm;

    public partial class CoverageEligibilityGroup
    {
        public int CoverageId { get;set; }

        public int EligibilityGroupId { get;set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        public virtual EligibilityGroup EligibilityGroup { get { return property0; } set { property0 = value; } }

        #region Private fields

        private CoverageEligibilityGroup clonedFrom;
        private EligibilityGroup field0;

        #endregion

        #region Constructors

        public CoverageEligibilityGroup(CoverageEligibilityGroup clonedFrom)
        {
            this.clonedFrom = clonedFrom;
        }

        public CoverageEligibilityGroup() { }

        #endregion

        #region Lazy properties

        private EligibilityGroup property0 { get;set; }

        #endregion
    }
}