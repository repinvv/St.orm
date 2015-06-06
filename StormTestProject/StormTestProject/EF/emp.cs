namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emp")]
    public partial class emp
    {
        public emp()
        {
            emp1 = new HashSet<emp>();
            emp2 = new HashSet<emp>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(9)]
        public string ssn { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string client { get; set; }

        public virtual ICollection<emp> emp1 { get; set; }

        public virtual ICollection<emp> emp2 { get; set; }
    }
}
