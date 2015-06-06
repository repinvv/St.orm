namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("currency")]
    public partial class currency
    {
        public currency()
        {
            policy = new HashSet<policy>();
        }

        [Key]
        public int currency_id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        [Required]
        [StringLength(10)]
        public string currency_code { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual ICollection<policy> policy { get; set; }
    }
}
