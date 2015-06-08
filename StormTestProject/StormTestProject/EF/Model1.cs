namespace StormTestProject.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<country> country { get; set; }
        public virtual DbSet<coverage_eligibility_group> coverage_eligibility_group { get; set; }
        public virtual DbSet<currency> currency { get; set; }
        public virtual DbSet<department> department { get; set; }
        public virtual DbSet<eligibility_group> eligibility_group { get; set; }
        public virtual DbSet<emp> emp { get; set; }
        public virtual DbSet<comment> comment { get; set; }
        public virtual DbSet<coverage> coverage { get; set; }
        public virtual DbSet<covered> covered { get; set; }
        public virtual DbSet<policy> policy { get; set; }
        public virtual DbSet<premium> premium { get; set; }
        public virtual DbSet<tax> tax { get; set; }
        public virtual DbSet<calculation> calculation { get; set; }
        public virtual DbSet<calculation_details> calculation_details { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<country>()
                .HasMany(e => e.policy)
                .WithRequired(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<eligibility_group>()
                .HasMany(e => e.coverage_eligibility_group)
                .WithRequired(e => e.eligibility_group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<emp>()
                .Property(e => e.ssn)
                .IsFixedLength();

            modelBuilder.Entity<emp>()
                .HasMany(e => e.emp1)
                .WithMany(e => e.emp2)
                .Map(m => m.ToTable("emp_to_dependent").MapLeftKey(new[] { "ssn", "client" }).MapRightKey(new[] { "dep_ssn", "dep_client" }));

            modelBuilder.Entity<coverage>()
                .HasMany(e => e.coverage_eligibility_group)
                .WithRequired(e => e.coverage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<coverage>()
                .HasMany(e => e.covered)
                .WithRequired(e => e.coverage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<coverage>()
                .HasMany(e => e.premium)
                .WithRequired(e => e.coverage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<coverage>()
                .HasMany(e => e.department)
                .WithMany(e => e.coverage)
                .Map(m => m.ToTable("coverage_department", "model").MapLeftKey("coverage_id").MapRightKey("department_id"));

            modelBuilder.Entity<policy>()
                .HasMany(e => e.coverage)
                .WithRequired(e => e.policy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<policy>()
                .HasMany(e => e.tax)
                .WithRequired(e => e.policy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<premium>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tax>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<calculation>()
                .HasMany(e => e.calculation_details)
                .WithRequired(e => e.calculation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<calculation_details>()
                .Property(e => e.value)
                .HasPrecision(18, 0);
        }
    }
}
