namespace StormCITest.EFSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class entity_with_multikey
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(256)]
        public string id_2 { get; set; }

        public string content { get; set; }
    }
}
