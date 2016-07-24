namespace StormCITest.EFSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class smallentity_with_sequence
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(1)]
        public string a_char { get; set; }

        [Required]
        [StringLength(2000)]
        public string a_varchar { get; set; }

        [Column(TypeName = "text")]
        public string a_text { get; set; }
    }
}
