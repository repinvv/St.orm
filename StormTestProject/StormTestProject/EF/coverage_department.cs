namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class coverage_department
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int coverage_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int department_id { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual coverage coverage { get; set; }

        public virtual department department { get; set; }
    }
}
