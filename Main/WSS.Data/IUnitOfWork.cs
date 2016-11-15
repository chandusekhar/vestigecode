using System;

namespace WSS.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<Setting> SettingRepository { get; }
        IDataRepository<AdditionalEmailAddress> AdditionalEmailAddressRepository { get; }
        IDataRepository<LinkedUtilityAccount> LinkedUtilityAccountsRepository { get; }
        IDataRepository<SiteContent> SiteContentRepository { get; }
        //IDataRepository<Status> StatusRepository { get; }
        IDataRepository<SubscriptionTransaction> SubscriptionTransactionRepository { get; }
        IDataRepository<WssAccount> WssAccountRepository { get; }
        IDataRepository<AuditRecord> WssAuditRecordRepository { get; }
        IDataRepository<Action> ActionDataRepository { get; }
        IDataRepository<SubscriptionTransactionStatus> SubscriptionStatusRepository { get; }
        IDataRepository<SubscriptionTransactionType> SubscriptionTypeRepository { get; }
        IDataRepository<WssAccountStatus> WssAccountStatusRepository { get; }
        IDataRepository<AuditSubject> AuditSubjectRepository { get; }
        IDataRepository<AuditEventWssAccount> AuditEventWssAccountRepository { get; }

        void Save(string userId);
    }
}