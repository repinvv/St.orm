namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("policy")]
    public partial class policy
    {
        public policy()
        {
            comment = new HashSet<comment>();
            coverage = new HashSet<coverage>();
            tax = new HashSet<tax>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int policy_id { get; set; }

        public int country_id { get; set; }

        public int currency_id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual ICollection<comment> comment { get; set; }

        public virtual country country { get; set; }

        public virtual ICollection<coverage> coverage { get; set; }

        public virtual currency currency { get; set; }

        public virtual ICollection<tax> tax { get; set; }
    }
}
