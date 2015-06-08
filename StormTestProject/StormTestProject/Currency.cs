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

    [Table("currency")]
    public partial class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("currency_id", Order = 1)]
        public int CurrencyId { get;set; }

        [Required]
        [MaxLength(256)]
        [Column("name", Order = 2)]
        public string Name { get;set; }

        [Required]
        [MaxLength(10)]
        [Column("currency_code", Order = 3)]
        public string CurrencyCode { get;set; }

        [Column("created", Order = 4)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 5)]
        public DateTime Updated { get;set; }

        public virtual ICollection<Policy> Policies { get { return property0; } set { property0 = value; } }

        #region Private fields

        private Currency clonedFrom;
        private Policy field0;

        #endregion

        #region Constructors

        public Currency(Currency clonedFrom)
        {
            this.clonedFrom = clonedFrom;
        }

        public Currency() { }

        #endregion

        #region Lazy properties

        private ICollection<Policy> property0 { get;set; }

        #endregion
    }
}
