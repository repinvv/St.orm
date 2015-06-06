namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("stats.calculation")]
    public partial class calculation
    {
        public calculation()
        {
            calculation_details = new HashSet<calculation_details>();
        }

        [Key]
        public Guid calculation_id { get; set; }

        [StringLength(256)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? due_date { get; set; }

        public virtual ICollection<calculation_details> calculation_details { get; set; }
    }
}
