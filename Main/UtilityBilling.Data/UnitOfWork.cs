using System;
using System.Data.Entity.Validation;
using System.Text;

namespace UtilityBilling.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly UtilityBillingContext _context;
       // private IDataRepository<Status> _status;
        private IDataRepository<DocumentHeader> _documentHeader;
        private IDataRepository<DocumentDetail> _documentDetail;
        private IDataRepository<UtilityAccount> _utilityAccount;
        private IDataRepository<DocumentType> _documentType;
        private IDataRepository<DocumentStatus> _documentStatus;
        private IDataRepository<UtilityAccountSource> _utilityAccountSource;
        private IDataRepository<AccountMeterLookup> _accountMeterLookup;

        public UnitOfWork(UtilityBillingContext context)
        {
            _context = context;
            _context.Configuration.AutoDetectChangesEnabled = true;
        }

        public IDataRepository<DocumentHeader> DocumentHeaderRepository => _documentHeader ?? (_documentHeader = new DataRepository<DocumentHeader>(_context));

        public IDataRepository<DocumentDetail> DocumentDetailRepository => _documentDetail ?? (_documentDetail = new DataRepository<DocumentDetail>(_context));

        public IDataRepository<UtilityAccount> UtilityAccountRepository => _utilityAccount ?? (_utilityAccount = new DataRepository<UtilityAccount>(_context));

        public IDataRepository<DocumentType> DocumentTypeRepository => _documentType ?? (_documentType = new DataRepository<DocumentType>(_context));

        public IDataRepository<DocumentStatus> DocumentStatusRepository => _documentStatus ?? (_documentStatus = new DataRepository<DocumentStatus>(_context));

        public IDataRepository<UtilityAccountSource> UtilityAccountSourceRepository => _utilityAccountSource ?? (_utilityAccountSource = new DataRepository<UtilityAccountSource>(_context));

        public IDataRepository<AccountMeterLookup> AccountMeterLookupRepository => _accountMeterLookup ?? (_accountMeterLookup = new DataRepository<AccountMeterLookup>(_context));

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

        #region IDisposable Methods

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