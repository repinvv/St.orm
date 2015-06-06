namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("covered")]
    public partial class covered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int covered_id { get; set; }

        public int covered_type { get; set; }

        public int coverage_id { get; set; }

        public int headcount { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual coverage coverage { get; set; }
    }
}
