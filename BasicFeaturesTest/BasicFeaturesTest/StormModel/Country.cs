//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BasicFeaturesTest.StormModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Country : IEquatable<Country>, ICloneable<Country>, IHaveId, IDbEntity
    {
        public int CountryId { get;set; }

        public string Name { get;set; }

        public string CountryCode { get;set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        #region Navigation properties
        public virtual IList<Policy> Policies
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
                        .Join(sourceQuery, x => x.CountryId, x => x.CountryId, (x, y) => x);
                };
                var items = loadService.GetList<Policy, Policy, int?>(0, query, x => x.CountryId, CountryId);
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
        IQueryable<Country> sourceQuery;
        private readonly Country clonedFrom;
        private IList<Policy> field0;
        #endregion

        #region Constructors
        public Country(Country clonedFrom, IQueryable<Country> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public Country(IQueryable<Country> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public Country()
        {
            field0 = new List<Policy>();
            populated = new bool[]{true};
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

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as Country);
        }

        public bool Equals(Country other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return CountryId == other.CountryId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return CountryId.GetHashCode();
            }
        }

        public static bool operator ==(Country left, Country right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Country left, Country right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
