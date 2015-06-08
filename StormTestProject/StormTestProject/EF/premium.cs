namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("model.premium")]
    public partial class premium
    {
        public premium()
        {
            comment = new HashSet<comment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int premium_id { get; set; }

        public int premium_type { get; set; }

        public int coverage_id { get; set; }

        public decimal amount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime updated { get; set; }

        public virtual ICollection<comment> comment { get; set; }

        public virtual coverage coverage { get; set; }
    }
}
