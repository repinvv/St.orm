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

    [Table("department")]
    public partial class Department
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

        #region Private fields

        private Department clonedFrom;

        #endregion

        #region Constructors

        public Department(Department clonedFrom)
        {
            this.clonedFrom = clonedFrom;
        }

        public Department() { }

        #endregion

        #region Lazy properties

        #endregion
    }
}
