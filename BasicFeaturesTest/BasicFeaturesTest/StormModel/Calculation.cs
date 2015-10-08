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

    public partial class Calculation : IEquatable<Calculation>, ICloneable<Calculation>, IHaveId, IDbEntity
    {
        public Guid CalculationId { get;set; }

        public string Name { get;set; }

        public DateTime? DueDate { get;set; }

        #region Navigation properties
        public virtual IList<CalculationDetails> CalculationDetailses
        {
            #region implementation
            get
            {
                if(populated[0] || loadService == null)
                {
                    return field0;
                }

                Func<IQueryable<CalculationDetails>> query = () =>
                {
                    return loadService.Context.Set<CalculationDetails>()
                        .Join(sourceQuery, x => x.CalculationId, x => x.CalculationId, (x, y) => x);
                };
                var items = loadService.GetList<CalculationDetails, CalculationDetails, Guid>(0, query, x => x.CalculationId, CalculationId);
                if (clonedFrom == null)
                {
                    field0 = items;
                }
                else
                {
                    clonedFrom.CalculationDetailses = items;
                    field0 = new List<CalculationDetails>(items.Count);
                    var repo = loadService.Context.GetDalRepository<CalculationDetails, CalculationDetails>();
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
        IQueryable<Calculation> sourceQuery;
        private readonly Calculation clonedFrom;
        private IList<CalculationDetails> field0;
        #endregion

        #region Constructors
        public Calculation(Calculation clonedFrom, IQueryable<Calculation> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public Calculation(IQueryable<Calculation> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
            populated = new bool[1];
        }

        public Calculation()
        {
            field0 = new List<CalculationDetails>();
            populated = new bool[]{true};
        }
        #endregion

        #region ICloneable implementation
        Calculation ICloneable<Calculation>.Clone()
        {
            return new Calculation(this, sourceQuery, loadService)
            {
                CalculationId = CalculationId,
                Name = Name,
                DueDate = DueDate,
            };
        }

        Calculation ICloneable<Calculation>.ClonedFrom()
        {
            return clonedFrom;
        }

        bool[] ICloneable<Calculation>.GetPopulated()
        {
            return populated;
        }
        #endregion

        #region Equality members
        public override bool Equals(object obj)
        {
            return Equals(obj as Calculation);
        }

        public bool Equals(Calculation other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return CalculationId == other.CalculationId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return CalculationId.GetHashCode();
            }
        }

        public static bool operator ==(Calculation left, Calculation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Calculation left, Calculation right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}
