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
    using System.Linq;
    using St.Orm;
    using St.Orm.Interfaces;

    [Table("model.coverage_department")]
    public partial class CoverageDepartment : ICloneable<CoverageDepartment>
    {
        [Key]
        [Column("coverage_id", Order = 1)]
        public int CoverageId { get;set; }

        [Key]
        [Column("department_id", Order = 2)]
        public int DepartmentId { get;set; }

        [Column("created", Order = 3)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 4)]
        public DateTime Updated { get;set; }

        public virtual Department Department { get { return property0; } set { property0 = value; } }

        #region Private fields

        private readonly bool[] populated = new bool[1];
        private readonly ILoadService loadService;
        IQueryable<CoverageDepartment> sourceQuery;
        private readonly CoverageDepartment clonedFrom;
        private Department field0;

        #endregion

        #region Constructors

        public CoverageDepartment(CoverageDepartment clonedFrom, IQueryable<CoverageDepartment> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public CoverageDepartment(IQueryable<CoverageDepartment> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public CoverageDepartment()
        {
        }

        #endregion

        #region ICloneable implementation

        CoverageDepartment ICloneable<CoverageDepartment>.Clone()
        {
            return new CoverageDepartment(this, sourceQuery, loadService)
            {
                CoverageId = CoverageId,
                DepartmentId = DepartmentId,
                Created = Created,
                Updated = Updated,
            };
        }

        CoverageDepartment ICloneable<CoverageDepartment>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<CoverageDepartment>.GetPopulated()
        {
            return populated;
        }

        #endregion

        #region Lazy properties

        private Department property0 { get;set; }


        #endregion
    }
}
