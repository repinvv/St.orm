namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("department")]
    public partial class department
    {
        public department()
        {
            coverage_department = new HashSet<coverage_department>();
        }

        [Key]
        public int department_id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual ICollection<coverage_department> coverage_department { get; set; }
    }
}
