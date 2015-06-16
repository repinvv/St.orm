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

    [Table("currency")]
    public partial class Currency : ICloneable<Currency>
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

        private readonly bool[] populated = new bool[1];
        private readonly ILoadService loadService;
        IQueryable<Currency> sourceQuery;
        private readonly Currency clonedFrom;
        private Policy field0;

        #endregion

        #region Constructors

        public Currency(Currency clonedFrom, IQueryable<Currency> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Currency(IQueryable<Currency> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Currency()
        {
            Policies = new HashSet<Policy>();
        }

        #endregion

        #region ICloneable implementation

        Currency ICloneable<Currency>.Clone()
        {
            return new Currency(this, sourceQuery, loadService)
            {
                CurrencyId = CurrencyId,
                Name = Name,
                CurrencyCode = CurrencyCode,
                Created = Created,
                Updated = Updated,
            };
        }

        Currency ICloneable<Currency>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Currency>.GetPopulated()
        {
            return populated;
        }

        #endregion

        #region Lazy properties

        private ICollection<Policy> property0 { get;set; }


        #endregion
    }
}
