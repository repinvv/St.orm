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

    [Table("department")]
    public partial class Department : ICloneable<Department>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("department_id", Order = 1)]
        public int DepartmentId { get;set; }

        [Required]
        [MaxLength(256)]
        [Column("name", Order = 2)]
        public string Name { get;set; }

        [Column("created", Order = 3)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 4)]
        public DateTime Updated { get;set; }

        #region Navigation properties
        #endregion

        #region Private fields
        private readonly ILoadService loadService;
        IQueryable<Department> sourceQuery;
        private readonly Department clonedFrom;
        #endregion

        #region Constructors
        public Department(Department clonedFrom, IQueryable<Department> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Department(IQueryable<Department> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Department()
        {
        }
        #endregion

        #region ICloneable implementation
        Department ICloneable<Department>.Clone()
        {
            return new Department(this, sourceQuery, loadService)
            {
                DepartmentId = DepartmentId,
                Name = Name,
                Created = Created,
                Updated = Updated,
            };
        }

        Department ICloneable<Department>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Department>.GetPopulated()
        {
            return null;
        }
        #endregion
    }
}
