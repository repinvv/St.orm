namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stats.calculation_details")]
    public partial class calculation_details
    {
        [Key]
        public Guid calculation_details_id { get; set; }

        public Guid calculation_id { get; set; }

        public int year { get; set; }

        public int month { get; set; }

        public decimal value { get; set; }

        public virtual calculation calculation { get; set; }
    }
}
