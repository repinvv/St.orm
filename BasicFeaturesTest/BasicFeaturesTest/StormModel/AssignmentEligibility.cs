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
    using System.Linq;

    public partial class AssignmentEligibility : IEquatable<AssignmentEligibility>, ICloneable<AssignmentEligibility>, IDbEntity
    {
        public int AssignmentId { get;set; }

        public int EligibilityId { get;set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        #region Navigation properties
        public virtual Eligibility Eligibility
        {
            #region implementation
            get
            {
                if(populated[0] || loadService == null)
                {
                    return field0;
                }

                Func<IQueryable<Eligibility>> query = () =>
                {
                    return loadService.Context.Set<Eligibility>()
                        .Join(sourceQuery, x => x.EligibilityId, x => x.EligibilityId, (x, y) => x);
                };
                var item = loadService.GetSingle<Eligibility, Eligibility, int>(0, query, x => x.EligibilityId, EligibilityId);
                if (clonedFrom == null)
                {
                    field0 = item;
                }
                else
                {
                    clonedFrom.Eligibility = item;
                    field0 = loadService.Context.GetDalRepository<Eligibility, Eligibility>().Clone(item);
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
        IQueryable<AssignmentEligibility> sourceQuery;
        private readonly AssignmentEligibility clonedFrom;
        private Eligibility field0;
        #endregion

        #region Constructors
        public AssignmentEligibility(AssignmentEligibility clonedFrom, IQueryable<AssignmentEligibility> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public AssignmentEligibility(IQueryable<AssignmentEligibility> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public AssignmentEligibility()
        {
            populated = new bool[]{true};
        }
        #endregion

        #region ICloneable implementation
        AssignmentEligibility ICloneable<AssignmentEligibility>.Clone()
        {
            return new AssignmentEligibility(this, sourceQuery, loadService)
            {
                AssignmentId = AssignmentId,
                EligibilityId = EligibilityId,
                Created = Created,
                Updated = Updated,
            };
        }

        AssignmentEligibility ICloneable<AssignmentEligibility>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<AssignmentEligibility>.GetPopulated()
        {
            return populated;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as AssignmentEligibility);
        }

        public bool Equals(AssignmentEligibility other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            bool equal = AssignmentId == other.AssignmentId;
            return equal && EligibilityId == other.EligibilityId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = AssignmentId.GetHashCode();
                return (hash * 397) ^ EligibilityId.GetHashCode();
            }
        }

        public static bool operator ==(AssignmentEligibility left, AssignmentEligibility right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssignmentEligibility left, AssignmentEligibility right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
