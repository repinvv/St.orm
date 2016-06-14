namespace StormCITest.EFSchema
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StormCI : DbContext
    {
        public StormCI()
            : base("name=StormCIContext")
        {
        }

        public virtual DbSet<entity_with_guid> entity_with_guid { get; set; }
        public virtual DbSet<entity_with_id> entity_with_id { get; set; }
        public virtual DbSet<entity_with_sequence> entity_with_sequence { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<entity_with_id>()
                .Property(e => e.a_numeric)
                .HasPrecision(6, 2);

            modelBuilder.Entity<entity_with_id>()
                .Property(e => e.a_decimal)
                .HasPrecision(8, 3);

            modelBuilder.Entity<entity_with_id>()
                .Property(e => e.a_smallmoney)
                .HasPrecision(10, 4);

            modelBuilder.Entity<entity_with_id>()
                .Property(e => e.a_money)
                .HasPrecision(19, 4);

            modelBuilder.Entity<entity_with_sequence>()
                .Property(e => e.a_char)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<entity_with_sequence>()
                .Property(e => e.a_varchar)
                .IsUnicode(false);

            modelBuilder.Entity<entity_with_sequence>()
                .Property(e => e.a_text)
                .IsUnicode(false);

            modelBuilder.Entity<entity_with_sequence>()
                .Property(e => e.a_nchar)
                .IsFixedLength();

            modelBuilder.Entity<entity_with_sequence>()
                .Property(e => e.a_binary)
                .IsFixedLength();
        }
    }
}
