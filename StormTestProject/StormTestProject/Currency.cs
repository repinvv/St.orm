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

        #region Navigation properties
        public virtual ICollection<Policy> Policies
        {
            #region implementation
            get
            {
                if(populated[0] || loadService == null)
                {
                    return field0;
                }

                Func<IQueryable<Policy>> query = () =>
                {
                    return loadService.Context.Set<Policy>()
                        .Join(sourceQuery, x => x.CurrencyId, x => x.CurrencyId, (x, y) => x);
                };
                var items = loadService.GetProperty<Policy, Policy, int>(0, query, x => x.CurrencyId, CurrencyId);
                if (clonedFrom == null)
                {
                    field0 = items;
                }
                else
                {
                    clonedFrom.Policies = items;
                    field0 = new List<Policy>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Policy, Policy>();
                    foreach(var item in items)
                    {
                        field0.Add(repo.Clone(item));
                    }
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
        private readonly bool[] populated;
        private readonly ILoadService loadService;
        IQueryable<Currency> sourceQuery;
        private readonly Currency clonedFrom;
        private ICollection<Policy> field0;
        #endregion

        #region Constructors
        public Currency(Currency clonedFrom, IQueryable<Currency> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public Currency(IQueryable<Currency> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public Currency()
        {
            field0 = new HashSet<Policy>();
            populated = new bool[]{true};
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
    }
}
