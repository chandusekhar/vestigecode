namespace UtilityBilling.Data
{
    using System.Data.Entity;

    public partial class UtilityBillingContext : DbContext
    {
        public UtilityBillingContext()
            : base("name=UtilityBillingDB")
        {
            Database.SetInitializer<UtilityBillingContext>(null);
        }

        public virtual DbSet<DocumentDetail> DocumentDetails { get; set; }
        public virtual DbSet<DocumentHeader> DocumentHeaders { get; set; }
        public virtual DbSet<UtilityAccount> UtilityAccounts { get; set; }
        public virtual DbSet<DomainLookup> DomainLookups { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public virtual DbSet<UtilityAccountSource> UtilityAccountSources { get; set; }
        public virtual DbSet<AccountMeterLookup> AccountMeterLookup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentHeader>()
                .Property(e => e.BillAmmountDue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DocumentHeader>()
                .HasMany(e => e.DocumentDetails)
                .WithRequired(e => e.DocumentHeader)
                .WillCascadeOnDelete(false);
        }
    }
}