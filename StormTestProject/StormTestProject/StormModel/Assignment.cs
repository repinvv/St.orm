//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject.StormModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using St.Orm;
    using St.Orm.Interfaces;

    [Table("assignment", Schema = "model")]
    public partial class Assignment : ICloneable<Assignment>, IEquatable<Assignment>, IHaveId, IDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("assignment_id", Order = 1)]
        public int AssignmentId { get;set; }

        [Column("policy_id", Order = 2)]
        public int PolicyId { get;set; }

        [MaxLength(256)]
        [Column("comment", Order = 3)]
        public string Comment { get;set; }

        [Column("created", Order = 4)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 5)]
        public DateTime Updated { get;set; }

        #region Navigation properties
        public virtual IList<Department> Departments
        {
            #region implementation
            get
            {
                if(populated[0] || loadService == null)
                {
                    return field0;
                }

                Func<IQueryable<AssignmentDepartment>> query = () =>
                {
                    return loadService.Context.Set<AssignmentDepartment>()
                        .Join(sourceQuery, x => x.AssignmentId, x => x.AssignmentId, (x, y) => x);
                };
                var items = loadService.GetList<AssignmentDepartment, AssignmentDepartment, int>(0, query, x => x.AssignmentId, AssignmentId)
                    .Select(x => x.Department)
                    .ToList();
                if (clonedFrom == null)
                {
                    field0 = items;
                }
                else
                {
                    clonedFrom.Departments = items;
                    field0 = new List<Department>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Department, Department>();
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

        public virtual IList<Eligibility> Eligibilities
        {
            #region implementation
            get
            {
                if(populated[1] || loadService == null)
                {
                    return field1;
                }

                Func<IQueryable<AssignmentEligibility>> query = () =>
                {
                    return loadService.Context.Set<AssignmentEligibility>()
                        .Join(sourceQuery, x => x.AssignmentId, x => x.AssignmentId, (x, y) => x);
                };
                var items = loadService.GetList<AssignmentEligibility, AssignmentEligibility, int>(1, query, x => x.AssignmentId, AssignmentId)
                    .Select(x => x.Eligibility)
                    .ToList();
                if (clonedFrom == null)
                {
                    field1 = items;
                }
                else
                {
                    clonedFrom.Eligibilities = items;
                    field1 = new List<Eligibility>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Eligibility, Eligibility>();
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

        public virtual IList<Premium> Premiums
        {
            #region implementation
            get
            {
                if(populated[2] || loadService == null)
                {
                    return field2;
                }

                Func<IQueryable<Premium>> query = () =>
                {
                    return loadService.Context.Set<Premium>()
                        .Join(sourceQuery, x => x.AssignmentId, x => x.AssignmentId, (x, y) => x);
                };
                var items = loadService.GetList<Premium, Premium, int>(2, query, x => x.AssignmentId, AssignmentId);
                if (clonedFrom == null)
                {
                    field2 = items;
                }
                else
                {
                    clonedFrom.Premiums = items;
                    field2 = new List<Premium>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Premium, Premium>();
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

        public virtual IList<Covered> Covereds
        {
            #region implementation
            get
            {
                if(populated[3] || loadService == null)
                {
                    return field3;
                }

                Func<IQueryable<Covered>> query = () =>
                {
                    return loadService.Context.Set<Covered>()
                        .Join(sourceQuery, x => x.AssignmentId, x => x.AssignmentId, (x, y) => x);
                };
                var items = loadService.GetList<Covered, Covered, int>(3, query, x => x.AssignmentId, AssignmentId);
                if (clonedFrom == null)
                {
                    field3 = items;
                }
                else
                {
                    clonedFrom.Covereds = items;
                    field3 = new List<Covered>(items.Count);
                    var repo = loadService.Context.GetDalRepository<Covered, Covered>();
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
        IQueryable<Assignment> sourceQuery;
        private readonly Assignment clonedFrom;
        private IList<Department> field0;
        private IList<Eligibility> field1;
        private IList<Premium> field2;
        private IList<Covered> field3;
        #endregion

        #region Constructors
        public Assignment(Assignment clonedFrom, IQueryable<Assignment> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[4];
        }

        public Assignment(IQueryable<Assignment> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[4];
        }

        public Assignment()
        {
            field0 = new List<Department>();
            field1 = new List<Eligibility>();
            field2 = new List<Premium>();
            field3 = new List<Covered>();
            populated = new bool[]{true, true, true, true};
        }
        #endregion

        #region ICloneable implementation
        Assignment ICloneable<Assignment>.Clone()
        {
            return new Assignment(this, sourceQuery, loadService)
            {
                AssignmentId = AssignmentId,
                PolicyId = PolicyId,
                Comment = Comment,
                Created = Created,
                Updated = Updated,
            };
        }

        Assignment ICloneable<Assignment>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Assignment>.GetPopulated()
        {
            return populated;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as Assignment);
        }

        public bool Equals(Assignment other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return AssignmentId == other.AssignmentId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return AssignmentId.GetHashCode();
            }
        }

        public static bool operator ==(Assignment left, Assignment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Assignment left, Assignment right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
