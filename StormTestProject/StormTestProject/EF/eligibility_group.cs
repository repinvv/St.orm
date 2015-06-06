namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class eligibility_group
    {
        public eligibility_group()
        {
            coverage_eligibility_group = new HashSet<coverage_eligibility_group>();
        }

        [Key]
        public int eligibility_group_id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual ICollection<coverage_eligibility_group> coverage_eligibility_group { get; set; }
    }
}
