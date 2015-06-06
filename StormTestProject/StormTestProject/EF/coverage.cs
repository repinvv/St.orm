namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("coverage")]
    public partial class coverage
    {
        public coverage()
        {
            coverage_department = new HashSet<coverage_department>();
            coverage_eligibility_group = new HashSet<coverage_eligibility_group>();
            covered = new HashSet<covered>();
            premium = new HashSet<premium>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int coverage_id { get; set; }

        public int policy_id { get; set; }

        [StringLength(256)]
        public string comment { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual ICollection<coverage_department> coverage_department { get; set; }

        public virtual ICollection<coverage_eligibility_group> coverage_eligibility_group { get; set; }

        public virtual policy policy { get; set; }

        public virtual ICollection<covered> covered { get; set; }

        public virtual ICollection<premium> premium { get; set; }
    }
}
