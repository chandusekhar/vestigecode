using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Text;

namespace WSS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WssApplicationContext _context;
        private IDataRepository<Action> _actionDataRepository;
        private IDataRepository<AdditionalEmailAddress> _additionalEmailAddress;
        private IDataRepository<LinkedUtilityAccount> _linkedUtilityAccounts;
        private IDataRepository<Setting> _setting;
        private IDataRepository<SiteContent> _siteContent;
        //private IDataRepository<Status> _status;
        private IDataRepository<SubscriptionTransaction> _subscriptionTransaction;
        private IDataRepository<WssAccount> _wssAccount;
        private IDataRepository<AuditRecord> _wssAuditRecords;
        private IDataRepository<SubscriptionTransactionStatus> _subscriptionStatuses;
        private IDataRepository<SubscriptionTransactionType> _subscriptionTypes;
        private IDataRepository<WssAccountStatus> _wssAccountStatuses;
        private IDataRepository<AuditSubject> _AuditSubjects;
        private IDataRepository<AuditEventWssAccount> _AuditEventWssAccounts;

        public UnitOfWork(WssApplicationContext context)
        {
            _context = context;
            _context.Configuration.AutoDetectChangesEnabled = true; // We are calling DetectChanges manually
        }

        public IDataRepository<Setting> SettingRepository
            => _setting ?? (_setting = new DataRepository<Setting>(_context));

        public IDataRepository<AdditionalEmailAddress> AdditionalEmailAddressRepository => _additionalEmailAddress ??
                                                                                           (_additionalEmailAddress =
                                                                                               new DataRepository
                                                                                                   <
                                                                                                       AdditionalEmailAddress
                                                                                                       >(_context));

        public IDataRepository<LinkedUtilityAccount> LinkedUtilityAccountsRepository => _linkedUtilityAccounts ??
                                                                                        (_linkedUtilityAccounts =
                                                                                            new DataRepository
                                                                                                <LinkedUtilityAccount>(
                                                                                                _context));

        public IDataRepository<SiteContent> SiteContentRepository
            => _siteContent ?? (_siteContent = new DataRepository<SiteContent>(_context));

       // public IDataRepository<WssAccountStatus> WssAccountStatusRepository => _status ?? (_status = new DataRepository<Status>(_context));

        public IDataRepository<SubscriptionTransaction> SubscriptionTransactionRepository => _subscriptionTransaction ??
                                                                                             (_subscriptionTransaction =
                                                                                                 new DataRepository
                                                                                                     <
                                                                                                         SubscriptionTransaction
                                                                                                         >(_context));

        public IDataRepository<WssAccount> WssAccountRepository
            => _wssAccount ?? (_wssAccount = new DataRepository<WssAccount>(_context));

        public IDataRepository<AuditRecord> WssAuditRecordRepository
            => _wssAuditRecords ?? (_wssAuditRecords = new DataRepository<AuditRecord>(_context));


        public IDataRepository<Action> ActionDataRepository
            => _actionDataRepository ?? (_actionDataRepository = new DataRepository<Action>(_context));

        public IDataRepository<SubscriptionTransactionStatus> SubscriptionStatusRepository
            => _subscriptionStatuses ?? (_subscriptionStatuses = new DataRepository<SubscriptionTransactionStatus>(_context));

        public IDataRepository<SubscriptionTransactionType> SubscriptionTypeRepository
            => _subscriptionTypes ?? (_subscriptionTypes = new DataRepository<SubscriptionTransactionType>(_context));

        public IDataRepository<WssAccountStatus> WssAccountStatusRepository
            => _wssAccountStatuses ?? (_wssAccountStatuses = new DataRepository<WssAccountStatus>(_context));

        public IDataRepository<AuditSubject> AuditSubjectRepository
            => _AuditSubjects ?? (_AuditSubjects = new DataRepository<AuditSubject>(_context));

        public IDataRepository<AuditEventWssAccount> AuditEventWssAccountRepository
            => _AuditEventWssAccounts ?? (_AuditEventWssAccounts = new DataRepository<AuditEventWssAccount>(_context));



        public void Save(string userId)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb, ex);
                // Add the original exception as the innerException
            }
        }

        private static void SetProperty(ObjectStateEntry entity, string field, object value)
        {
            var prop = entity.Entity.GetType().GetProperty(field);
            prop?.SetValue(entity.Entity, value);
        }

        #region IDisposable Methods

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Methods
    }
}