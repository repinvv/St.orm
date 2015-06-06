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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StormTest.dbo.covered")]
    public partial class Covered
    {
        [Key]
        [Column("covered_id", Order = 0)]
        public int CoveredId { get;set; }

        [Column("covered_type", Order = 1)]
        public int CoveredType { get;set; }

        [Column("coverage_id", Order = 2)]
        public int CoverageId { get;set; }

        [Column("headcount", Order = 3)]
        public int Headcount { get;set; }

        [Column("created", Order = 4)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 5)]
        public DateTime Updated { get;set; }

        #region Private fields

        private Covered clonedFrom;

        #endregion

        #region Constructors

        public Covered(Covered clonedFrom)
        {
            this.clonedFrom = clonedFrom;
        }

        public Covered() { }

        #endregion

        #region Lazy properties

        #endregion
    }
}
