namespace WWDCommon.Data
{
    using System.Data.Entity;

    public partial class WDDCommonContext : DbContext
    {
        public WDDCommonContext()
            : base("name=WDDCommonDB")
        {
            //Database.SetInitializer<WDDCommonContext>();
        }

        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<EmailTransaction> EmailTransactions { get; set; }
        public virtual DbSet<EmailQueue> EmailQueues { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Functions> Functions { get; set; }
        public virtual DbSet<Application> Applications { get; set; }

        //public virtual DbSet<UserRoles> UserRoles { get; set; }
        //public virtual DbSet<RolesFunctions> RoleFunctions { get; set; }
        public virtual DbSet<ApplicationRoles> ApplicationRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailTransaction>()
                .Property(e => e.SourcePackage)
                .IsFixedLength();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}