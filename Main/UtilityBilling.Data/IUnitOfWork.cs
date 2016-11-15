using System;

namespace UtilityBilling.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<DocumentHeader> DocumentHeaderRepository { get; }
        IDataRepository<DocumentDetail> DocumentDetailRepository { get; }
        IDataRepository<UtilityAccount> UtilityAccountRepository { get; }
        IDataRepository<DocumentType> DocumentTypeRepository { get; }
        IDataRepository<DocumentStatus> DocumentStatusRepository { get; }
        IDataRepository<UtilityAccountSource> UtilityAccountSourceRepository { get; }
        IDataRepository<AccountMeterLookup> AccountMeterLookupRepository { get; }
        void Save(string userId);
    }
}