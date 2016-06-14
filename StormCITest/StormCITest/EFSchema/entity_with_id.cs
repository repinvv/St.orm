namespace StormCITest.EFSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class entity_with_id
    {
        public int id { get; set; }

        public long? a_bigint { get; set; }

        public int a_int { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? a_numeric { get; set; }

        public bool? a_bit { get; set; }

        public short? a_smallint { get; set; }

        public decimal? a_decimal { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? a_smallmoney { get; set; }

        public byte? a_tinyint { get; set; }

        [Column(TypeName = "money")]
        public decimal? a_money { get; set; }
    }
}
