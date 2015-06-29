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

    [Table("model.policy")]
    public partial class Policy : ICloneable<Policy>, IEquatable<Policy>, IHaveId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("policy_id", Order = 1)]
        public int PolicyId { get;set; }

        [Column("country_id", Order = 2)]
        public int? CountryId { get;set; }

        [Column("currency_id", Order = 3)]
        public int CurrencyId { get;set; }

        [Required]
        [MaxLength(256)]
        [Column("name", Order = 4)]
        public string Name { get;set; }

        [Column("created", Order = 5)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 6)]
        public DateTime Updated { get;set; }

        #region Navigation properties
        public virtual Country Country
        {
            #region implementation
            get
            {
                if(populated[0] || loadService == null)
                {
                    return field0;
                }

                Func<IQueryable<Country>> query = () =>
                {
                    return loadService.Context.Set<Country>()
                        .Join(sourceQuery, x => x.CountryId, x => x.CountryId, (x, y) => x);
                };
                var item = loadService.GetSingle<Country, Country, int>(0, query, x => x.CountryId, CountryId);
                if (clonedFrom == null)
                {
                    field0 = item;
                }
                else
                {
                    clonedFrom.Country = item;
                    field0 = loadService.Context.GetDalRepository<Country, Country>().Clone(item);
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

        public virtual ICollection<Tax> Taxes
        {
            #region implementation
            get
            {
                if(populated[1] || loadService == null)
                {
                    return field1;
                }

                Func<IQueryable<Tax>> query = () =>
                {
                    return loadService.Context.Set<Tax>()
                        .Join(sourceQuery, x => x.PolicyId, x => x.PolicyId, (x, y) => x);
                };
                var items = loadService.GetList<Tax, Tax, int>(1, query, x => x.PolicyId, PolicyId);
                if (clonedFrom == null)
                {
                    field1 = items;
                }
                else
                {
                    clonedFrom.Taxes = items;
                    field1 = new List<Tax>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Tax, Tax>();
                    foreach(var item in items)
                    {
                        field1.Add(repo.Clone(item));
                    }
                }

                populated[1] = true;
                return field1;
            }
            set
            {
                field1 = value;
                populated[1] = true;
            }
            #endregion
        }

        public virtual ICollection<Assignment> Assignments
        {
            #region implementation
            get
            {
                if(populated[2] || loadService == null)
                {
                    return field2;
                }

                Func<IQueryable<Assignment>> query = () =>
                {
                    return loadService.Context.Set<Assignment>()
                        .Join(sourceQuery, x => x.PolicyId, x => x.PolicyId, (x, y) => x);
                };
                var items = loadService.GetList<Assignment, Assignment, int>(2, query, x => x.PolicyId, PolicyId);
                if (clonedFrom == null)
                {
                    field2 = items;
                }
                else
                {
                    clonedFrom.Assignments = items;
                    field2 = new List<Assignment>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Assignment, Assignment>();
                    foreach(var item in items)
                    {
                        field2.Add(repo.Clone(item));
                    }
                }

                populated[2] = true;
                return field2;
            }
            set
            {
                field2 = value;
                populated[2] = true;
            }
            #endregion
        }

        public virtual ICollection<Comment> Comments
        {
            #region implementation
            get
            {
                if(populated[3] || loadService == null)
                {
                    return field3;
                }

                Func<IQueryable<Comment>> query = () =>
                {
                    return loadService.Context.Set<Comment>()
                        .Join(sourceQuery, x => x.PolicyId, x => x.PolicyId, (x, y) => x);
                };
                var items = loadService.GetList<Comment, Comment, int?>(3, query, x => x.PolicyId, PolicyId);
                if (clonedFrom == null)
                {
                    field3 = items;
                }
                else
                {
                    clonedFrom.Comments = items;
                    field3 = new List<Comment>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Comment, Comment>();
                    foreach(var item in items)
                    {
                        field3.Add(repo.Clone(item));
                    }
                }

                populated[3] = true;
                return field3;
            }
            set
            {
                field3 = value;
                populated[3] = true;
            }
            #endregion
        }

        #endregion

        #region Private fields
        private readonly bool[] populated;
        private readonly ILoadService loadService;
        IQueryable<Policy> sourceQuery;
        private readonly Policy clonedFrom;
        private Country field0;
        private ICollection<Tax> field1;
        private ICollection<Assignment> field2;
        private ICollection<Comment> field3;
        #endregion

        #region Constructors
        public Policy(Policy clonedFrom, IQueryable<Policy> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[4];
        }

        public Policy(IQueryable<Policy> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[4];
        }

        public Policy()
        {
            field1 = new HashSet<Tax>();
            field2 = new HashSet<Assignment>();
            field3 = new HashSet<Comment>();
            populated = new bool[]{true, true, true, true};
        }
        #endregion

        #region ICloneable implementation
        Policy ICloneable<Policy>.Clone()
        {
            return new Policy(this, sourceQuery, loadService)
            {
                PolicyId = PolicyId,
                CountryId = CountryId,
                CurrencyId = CurrencyId,
                Name = Name,
                Created = Created,
                Updated = Updated,
            };
        }

        Policy ICloneable<Policy>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Policy>.GetPopulated()
        {
            return populated;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as Policy);
        }

        public bool Equals(Policy other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return PolicyId == other.PolicyId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return PolicyId.GetHashCode();
            }
        }

        public static bool operator ==(Policy left, Policy right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Policy left, Policy right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
