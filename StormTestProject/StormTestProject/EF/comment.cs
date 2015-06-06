namespace StormTestProject.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("comment")]
    public partial class comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int comment_id { get; set; }

        public int comment_type { get; set; }

        public int? policy_id { get; set; }

        public int? premium_id { get; set; }

        [Required]
        public string comment_text { get; set; }

        public int author_user_id { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public virtual policy policy { get; set; }

        public virtual premium premium { get; set; }
    }
}
