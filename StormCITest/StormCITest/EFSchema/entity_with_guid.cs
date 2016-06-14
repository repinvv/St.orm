namespace StormCITest.EFSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class entity_with_guid
    {
        public Guid id { get; set; }

        public double? a_float { get; set; }

        public float a_real { get; set; }

        [Column(TypeName = "date")]
        public DateTime? a_date { get; set; }

        public TimeSpan? a_time { get; set; }

        public DateTimeOffset? a_offset { get; set; }

        public DateTime? a_datetime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? a_datetime2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? a_smalldatetime { get; set; }
    }
}
