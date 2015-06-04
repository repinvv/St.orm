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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("eligibility_group")]
    public partial class EligibilityGroup
    {
        private EligibilityGroup clonedFrom;

        public EligibilityGroup(EligibilityGroup clonedFrom)
        {
            this.clonedFrom = clonedFrom;
        }

        public EligibilityGroup() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("eligibility_group_id")]
        public int EligibilityGroupId { get;set; }

        [Required]
        [MaxLength(256)]
        [Column("name")]
        public string Name { get;set; }

        [Column("created")]
        public DateTime Created { get;set; }

        [Column("updated")]
        public DateTime Updated { get;set; }
    }
}
