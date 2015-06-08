//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject
{
    using System.Data.Entity;
    using St.Orm.Interfaces;

    public partial class StormTestContext : DbContext
    {
        public StormTestContext() : base("name=StormTestContext") { }

        public virtual DbSet<EligibilityGroup> EligibilityGroups { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Policy> Policies { get; set; }

        public virtual DbSet<Tax> Taxes { get; set; }

        public virtual DbSet<Coverage> Coverages { get; set; }

        public virtual DbSet<Premium> Premiums { get; set; }

        public virtual DbSet<Covered> Covereds { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Emp> Emps { get; set; }

        public virtual DbSet<EmpToDependent> EmpToDependents { get; set; }

        public virtual DbSet<Calculation> Calculations { get; set; }

        public virtual DbSet<CalculationDetails> CalculationDetailses { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            InitializeCurrencyRelations(modelBuilder);
            InitializeCountryRelations(modelBuilder);
            InitializePolicyRelations(modelBuilder);
            InitializeCoverageRelations(modelBuilder);
            InitializePremiumRelations(modelBuilder);
            InitializeEmpRelations(modelBuilder);
            InitializeCalculationRelations(modelBuilder);
        }

        protected virtual void InitializeCurrencyRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .HasMany(x => x.Policies)
                .WithOptional()
                .HasForeignKey(x => x.CurrencyId);
        }

        protected virtual void InitializeCountryRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(x => x.Policies)
                .WithRequired()
                .HasForeignKey(x => x.CountryId);
        }

        protected virtual void InitializePolicyRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>()
                .HasMany(x => x.Taxes)
                .WithRequired()
                .HasForeignKey(x => x.PolicyId);
            modelBuilder.Entity<Policy>()
                .HasMany(x => x.Coverages)
                .WithRequired()
                .HasForeignKey(x => x.PolicyId);
            modelBuilder.Entity<Policy>()
                .HasMany(x => x.Comments)
                .WithOptional()
                .HasForeignKey(x => x.PolicyId);
        }

        protected virtual void InitializeCoverageRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coverage>()
                .HasMany(x => x.EligibilityGroups)
                .WithMany()
                .Map(m => m.ToTable("coverage_eligibility_group", "dbo")
                    .MapLeftKey("coverage_id")
                    .MapRightKey("eligibility_group_id"));
            modelBuilder.Entity<Coverage>()
                .HasMany(x => x.Departments)
                .WithMany()
                .Map(m => m.ToTable("coverage_department", "model")
                    .MapLeftKey("coverage_id")
                    .MapRightKey("department_id"));
            modelBuilder.Entity<Coverage>()
                .HasMany(x => x.Premiums)
                .WithRequired()
                .HasForeignKey(x => x.CoverageId);
            modelBuilder.Entity<Coverage>()
                .HasMany(x => x.Covereds)
                .WithRequired()
                .HasForeignKey(x => x.CoverageId);
        }

        protected virtual void InitializePremiumRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Premium>()
                .HasMany(x => x.Comments)
                .WithOptional()
                .HasForeignKey(x => x.PremiumId);
        }

        protected virtual void InitializeEmpRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emp>()
                .HasMany(x => x.EmpToDependents)
                .WithRequired()
                .HasForeignKey(x => new { x.Ssn, x.Client });
            modelBuilder.Entity<Emp>()
                .HasMany(x => x.EmpToDependents2)
                .WithRequired()
                .HasForeignKey(x => new { x.DepSsn, x.DepClient });
        }

        protected virtual void InitializeCalculationRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calculation>()
                .HasMany(x => x.CalculationDetailses)
                .WithRequired()
                .HasForeignKey(x => x.CalculationId);
        }
    }
}
