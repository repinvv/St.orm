namespace StormCITest.EFSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("some_schema.entity_with_sequence")]
    public partial class entity_with_sequence
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

        [StringLength(10)]
        public string a_nchar { get; set; }

        public string a_nvarchar { get; set; }

        [Column(TypeName = "ntext")]
        public string a_ntext { get; set; }

        [Column(TypeName = "xml")]
        public string a_xml { get; set; }

        [MaxLength(1000)]
        public byte[] a_binary { get; set; }

        [MaxLength(2000)]
        public byte[] a_varbinary { get; set; }

        [Column(TypeName = "image")]
        public byte[] a_image { get; set; }
    }
}
