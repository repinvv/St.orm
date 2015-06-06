namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("country")]
    public partial class country
    {
        public country()
        {
            policy = new HashSet<policy>();
        }

        [Key]
        public int country_id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        [Required]
        [StringLength(10)]
        public string country_code { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual ICollection<policy> policy { get; set; }
    }
}
