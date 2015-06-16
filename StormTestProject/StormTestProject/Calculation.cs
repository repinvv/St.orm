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

    [Table("stats.calculation")]
    public partial class Calculation : ICloneable<Calculation>
    {
        [Key]
        [Column("calculation_id", Order = 1)]
        public Guid CalculationId { get;set; }

        [MaxLength(256)]
        [Column("name", Order = 2)]
        public string Name { get;set; }

        [Column("due_date", Order = 3)]
        public DateTime? DueDate { get;set; }

        public virtual ICollection<CalculationDetails> CalculationDetailses { get { return property0; } set { property0 = value; } }

        #region Private fields

        private readonly bool[] populated = new bool[1];
        private readonly ILoadService loadService;
        IQueryable<Calculation> sourceQuery;
        private readonly Calculation clonedFrom;
        private CalculationDetails field0;

        #endregion

        #region Constructors

        public Calculation(Calculation clonedFrom, IQueryable<Calculation> sourceQuery, ILoadService loadService)
        {
            this.clonedFrom = clonedFrom;
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Calculation(IQueryable<Calculation> sourceQuery, ILoadService loadService)
        {
            this.loadService = loadService;
            this.sourceQuery = sourceQuery;
        }

        public Calculation()
        {
            CalculationDetailses = new HashSet<CalculationDetails>();
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

        #region Lazy properties

        private ICollection<CalculationDetails> property0 { get;set; }


        #endregion
    }
}
