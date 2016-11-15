using System;
using System.Data.Entity.Validation;
using System.Text;

namespace WWDCommon.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WDDCommonContext _context;
        private bool _disposed;
        private IDataRepository<EmailTemplate> _emailTemplate;
        private IDataRepository<EmailTransaction> _emailTransaction;
        private IDataRepository<EmailQueue> _emailQueue;
        private IDataRepository<Users> _user;
        private IDataRepository<Roles> _role;
        private IDataRepository<Functions> _function;
        private IDataRepository<Application> _application;

        //private IDataRepository<UserRoles> _userroles;
        //private IDataRepository<RolesFunctions> _roleFunctions;
        private IDataRepository<ApplicationRoles> _applicationRoles;

        public UnitOfWork(WDDCommonContext context)
        {
            _context = context;
            _context.Configuration.AutoDetectChangesEnabled = false; // We are calling DetectChanges manually
        }

        public IDataRepository<EmailTemplate> EmailTemplateRepository
            => _emailTemplate ?? (_emailTemplate = new DataRepository<EmailTemplate>(_context));

        public IDataRepository<EmailTransaction> EmailTransaction
            => _emailTransaction ?? (_emailTransaction = new DataRepository<EmailTransaction>(_context));

        public IDataRepository<EmailQueue> EmailQueueRepository
            => _emailQueue ?? (_emailQueue = new DataRepository<EmailQueue>(_context));

        public IDataRepository<Users> UserRepository
    => _user ?? (_user = new DataRepository<Users>(_context));

        public IDataRepository<Roles> RolesRepository
    => _role ?? (_role = new DataRepository<Roles>(_context));

        public IDataRepository<Functions> FunctionRespository
    => _function ?? (_function = new DataRepository<Functions>(_context));

        public IDataRepository<Application> ApplicationRespository
    => _application ?? (_application = new DataRepository<Application>(_context));

        //      public IDataRepository<UserRoles> UserRolesRespository
        //   => _userroles ?? (_userroles = new DataRepository<UserRoles>(_context));

        //      public IDataRepository<RolesFunctions> RoleFunctionRespository
        //=> _roleFunctions ?? (_roleFunctions = new DataRepository<RolesFunctions>(_context));
        public IDataRepository<ApplicationRoles> ApplicationRolesRespository
  => _applicationRoles ?? (_applicationRoles = new DataRepository<ApplicationRoles>(_context));

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