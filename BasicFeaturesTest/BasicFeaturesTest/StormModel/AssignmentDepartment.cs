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

    public partial class AssignmentDepartment : IEquatable<AssignmentDepartment>, ICloneable<AssignmentDepartment>, IDbEntity
    {
        public int AssignmentId { get;set; }

        public int DepartmentId { get;set; }

        public DateTime Created { get;set; }

        public DateTime Updated { get;set; }

        #region Navigation properties
        public virtual Department Department
        {
            #region implementation
            get
            {
                if(populated[0] || loadService == null)
                {
                    return field0;
                }

                Func<IQueryable<Department>> query = () =>
                {
                    return loadService.Context.Set<Department>()
                        .Join(sourceQuery, x => x.DepartmentId, x => x.DepartmentId, (x, y) => x);
                };
                var item = loadService.GetSingle<Department, Department, int>(0, query, x => x.DepartmentId, DepartmentId);
                if (clonedFrom == null)
                {
                    field0 = item;
                }
                else
                {
                    clonedFrom.Department = item;
                    field0 = loadService.Context.GetDalRepository<Department, Department>().Clone(item);
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
        IQueryable<AssignmentDepartment> sourceQuery;
        private readonly AssignmentDepartment clonedFrom;
        private Department field0;
        #endregion

        #region Constructors
        public AssignmentDepartment(AssignmentDepartment clonedFrom, IQueryable<AssignmentDepartment> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public AssignmentDepartment(IQueryable<AssignmentDepartment> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public AssignmentDepartment()
        {
            populated = new bool[]{true};
        }
        #endregion

        #region ICloneable implementation
        AssignmentDepartment ICloneable<AssignmentDepartment>.Clone()
        {
            return new AssignmentDepartment(this, sourceQuery, loadService)
            {
                AssignmentId = AssignmentId,
                DepartmentId = DepartmentId,
                Created = Created,
                Updated = Updated,
            };
        }

        AssignmentDepartment ICloneable<AssignmentDepartment>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<AssignmentDepartment>.GetPopulated()
        {
            return populated;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as AssignmentDepartment);
        }

        public bool Equals(AssignmentDepartment other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            bool equal = AssignmentId == other.AssignmentId;
            return equal && DepartmentId == other.DepartmentId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = AssignmentId.GetHashCode();
                return (hash * 397) ^ DepartmentId.GetHashCode();
            }
        }

        public static bool operator ==(AssignmentDepartment left, AssignmentDepartment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssignmentDepartment left, AssignmentDepartment right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
