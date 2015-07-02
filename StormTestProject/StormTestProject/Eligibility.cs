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

    [Table("eligibility", Schema = "dbo")]
    public partial class Eligibility : ICloneable<Eligibility>, IEquatable<Eligibility>, IHaveId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("eligibility_id", Order = 1)]
        public int EligibilityId { get;set; }

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
        IQueryable<Eligibility> sourceQuery;
        private readonly Eligibility clonedFrom;
        #endregion

        #region Constructors
        public Eligibility(Eligibility clonedFrom, IQueryable<Eligibility> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Eligibility(IQueryable<Eligibility> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Eligibility()
        {
        }
        #endregion

        #region ICloneable implementation
        Eligibility ICloneable<Eligibility>.Clone()
        {
            return new Eligibility(this, sourceQuery, loadService)
            {
                EligibilityId = EligibilityId,
                Name = Name,
                Created = Created,
                Updated = Updated,
            };
        }

        Eligibility ICloneable<Eligibility>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Eligibility>.GetPopulated()
        {
            return null;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as Eligibility);
        }

        public bool Equals(Eligibility other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return EligibilityId == other.EligibilityId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return EligibilityId.GetHashCode();
            }
        }

        public static bool operator ==(Eligibility left, Eligibility right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Eligibility left, Eligibility right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
