using System;

namespace WWDCommon.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<EmailTemplate> EmailTemplateRepository { get; }
        IDataRepository<EmailTransaction> EmailTransaction { get; }
        IDataRepository<EmailQueue> EmailQueueRepository { get; }
        IDataRepository<Users> UserRepository { get; }
        IDataRepository<Roles> RolesRepository { get; }
        IDataRepository<Functions> FunctionRespository { get; }
        IDataRepository<Application> ApplicationRespository { get; }

        // IDataRepository<UserRoles> UserRolesRespository { get; }
        // IDataRepository<RolesFunctions> RoleFunctionRespository { get; }
        IDataRepository<ApplicationRoles> ApplicationRolesRespository { get; }

        void Save(string userId);
    }
}