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

    [Table("model.assignment_department")]
    public partial class AssignmentDepartment : ICloneable<AssignmentDepartment>
    {
        [Key]
        [Column("assignment_id", Order = 1)]
        public int AssignmentId { get;set; }

        [Key]
        [Column("department_id", Order = 2)]
        public int DepartmentId { get;set; }

        [Column("created", Order = 3)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 4)]
        public DateTime Updated { get;set; }

        #region Navigation properties
        public virtual Department Department
        {
            #region implementation
            get
            {
                if(populated[0])
                {
                    return field0;
                }

                populated[0] = true;
                return field0;
            }
            set
            {
                field0 = value;
                populated[0] = true;
            }
            #endregion
        }

        #endregion

        #region Private fields
        private readonly bool[] populated = new bool[1];
        private readonly ILoadService loadService;
        IQueryable<AssignmentDepartment> sourceQuery;
        private readonly AssignmentDepartment clonedFrom;
        private Department field0;
        #endregion

        #region Constructors
        public AssignmentDepartment(AssignmentDepartment clonedFrom, IQueryable<AssignmentDepartment> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public AssignmentDepartment(IQueryable<AssignmentDepartment> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public AssignmentDepartment()
        {
        }
        #endregion

        #region ICloneable implementation
        AssignmentDepartment ICloneable<AssignmentDepartment>.Clone()
        {
            return new AssignmentDepartment(this, sourceQuery, loadService)
            {
                AssignmentId = AssignmentId,
                DepartmentId = DepartmentId,
                Created = Created,
                Updated = Updated,
            };
        }

        AssignmentDepartment ICloneable<AssignmentDepartment>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<AssignmentDepartment>.GetPopulated()
        {
            return populated;
        }
        #endregion
    }
}
