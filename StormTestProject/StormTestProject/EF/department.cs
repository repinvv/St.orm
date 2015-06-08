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
            coverage = new HashSet<coverage>();
        }

        [Key]
        public int department_id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime updated { get; set; }

        public virtual ICollection<coverage> coverage { get; set; }
    }
}
