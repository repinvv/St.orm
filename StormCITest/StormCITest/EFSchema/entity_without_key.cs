namespace StormCITest.EFSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class entity_without_key
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int value { get; set; }

        public string content { get; set; }
    }
}
