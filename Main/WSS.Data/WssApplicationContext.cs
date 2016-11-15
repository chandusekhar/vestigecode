using System.Data.Entity.Core.Metadata.Edm;

namespace WSS.Data
{
    using System.Data.Entity;

    public partial class WssApplicationContext : DbContext//,IWSSContext
    {
        //private CoreTracker _coreTracker;

        //static WssApplicationContext()
        //{
        //    Database.SetInitializer<WssApplicationContext>(new WSSDBInitializer<WssApplicationContext>());
        //}

        public WssApplicationContext() : base("WssApplication")
        {
            Database.SetInitializer<WssApplicationContext>(null);
        }

        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<AdditionalEmailAddress> AdditionalEmailAddresses { get; set; }
        public virtual DbSet<LinkedUtilityAccount> LinkedUtilityAccounts { get; set; }
        public virtual DbSet<SiteContent> SiteContents { get; set; }
        //public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SubscriptionTransaction> SubscriptionTransactions { get; set; }
        public virtual DbSet<WssAccount> WssAccounts { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<AuditRecord> AuditRecords { get; set; }
        public virtual DbSet<DomainLookup> DomainLookups { get; set; }
        public virtual DbSet<SubscriptionTransactionStatus> SubscriptionTransactionStatuses { get; set; }
        public virtual DbSet<SubscriptionTransactionType> SubscriptionTransactionTypes { get; set; }
        public virtual DbSet<WssAccountStatus> WssAccountStatuses { get; set; }
        public DbSet<AuditSubject> vwDLU_AUDIT_SUBJECT { get; set; }
        public DbSet<AuditEventWssAccount> vwDLU_AUDIT_EVENT_WSS_ACCT  { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //    modelBuilder.HasDefaultSchema("wss");
            //    modelBuilder.Entity<vw_WSSUtilityAccount>().ToTable("vw_WSSUtilityAccount", "wss");
        }

        public new void SaveChanges()
        {
            //Pass username
            //var lstAuditLog = _coreTracker.AuditChanges("");
            //foreach (var item in lstAuditLog)
            //{
            //    this.AuditRecords.AddRange(AddAuditRecords(item));
            //}
            //IEnumerable<DbEntityEntry> addedEntries = _coreTracker.GetAdditions();
            //var lstAddedAuditLog = _coreTracker.AuditAdditions("", addedEntries);
            //foreach (var item in lstAddedAuditLog)
            //{
            //    this.AuditRecords.AddRange(AddAuditRecords(item));
            //}

            base.SaveChanges();
        }

        #region "Tracker Changes"

        //private void InitializeCoreTracker()
        //{
        //    _coreTracker = new CoreTracker(this);
        //}

        //private List<AuditRecord> AddAuditRecords(AuditLog auditLog)
        //{
        //    var lstAuditRecord = new List<AuditRecord>();
        //    foreach (var log in auditLog.LogDetails)
        //    {
        //        var auditRecord = new AuditRecord()
        //        {
        //            date = DateTime.Now,
        //            EventType = auditLog.EventType.ToString(),
        //            FieldName = log.PropertyName,
        //            NewValue = log.NewValue,
        //            OldValue = log.OriginalValue,
        //        };
        //        lstAuditRecord.Add(auditRecord);
        //    }
        //    return lstAuditRecord;
        //}

        #endregion "Tracker Changes"
    }
}