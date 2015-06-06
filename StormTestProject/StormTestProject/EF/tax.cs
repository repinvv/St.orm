namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tax")]
    public partial class tax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tax_id { get; set; }

        public int policy_id { get; set; }

        public decimal amount { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual policy policy { get; set; }
    }
}
