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

    [Table("model.assignment_eligibility")]
    public partial class AssignmentEligibility : ICloneable<AssignmentEligibility>
    {
        [Key]
        [Column("assignment_id", Order = 1)]
        public int AssignmentId { get;set; }

        [Key]
        [Column("eligibility_id", Order = 2)]
        public int EligibilityId { get;set; }

        [Column("created", Order = 3)]
        public DateTime Created { get;set; }

        [Column("updated", Order = 4)]
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
                var items = loadService.GetProperty<Eligibility, Eligibility, int>(0, query, x => x.EligibilityId, EligibilityId);
                var item = items.FirstOrDefault();
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
        private readonly bool[] populated = new bool[1];
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
        }

        public AssignmentEligibility(IQueryable<AssignmentEligibility> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public AssignmentEligibility()
        {
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
    }
}
