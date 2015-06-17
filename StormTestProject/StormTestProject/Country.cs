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

    [Table("country")]
    public partial class Country : ICloneable<Country>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("country_id", Order = 1)]
        public int CountryId { get;set; }

        [Required]
        [MaxLength(256)]
        [Column("name", Order = 2)]
        public string Name { get;set; }

        [Required]
        [MaxLength(10)]
        [Column("country_code", Order = 3)]
        public string CountryCode { get;set; }

        [Column("created", Order = 4)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 5)]
        public DateTime Updated { get;set; }

        public virtual ICollection<Policy> Policies { get { return property0; } set { property0 = value; } }

        #region Private fields

        private readonly bool[] populated = new bool[1];
        private readonly ILoadService loadService;
        IQueryable<Country> sourceQuery;
        private readonly Country clonedFrom;
        private Policy field0;

        #endregion

        #region Constructors

        public Country(Country clonedFrom, IQueryable<Country> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Country(IQueryable<Country> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Country()
        {
            Policies = new HashSet<Policy>();
        }

        #endregion

        #region ICloneable implementation

        Country ICloneable<Country>.Clone()
        {
            return new Country(this, sourceQuery, loadService)
            {
                CountryId = CountryId,
                Name = Name,
                CountryCode = CountryCode,
                Created = Created,
                Updated = Updated,
            };
        }

        Country ICloneable<Country>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Country>.GetPopulated()
        {
            return populated;
        }

        #endregion

        #region Lazy properties

        private ICollection<Policy> property0 { get;set; }

        #endregion
    }
}
