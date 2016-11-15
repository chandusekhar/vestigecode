using System.Collections.Generic;
using WSS.Data;

namespace WSS.AuditTransaction.Interfaces
{
    public interface IAuditTransaction
    {
        IEnumerable<AuditRecord> GetAuditRecords();

        IEnumerable<AuditRecord> GetAuditRecords(int wssAccountId);

        void AddAuditRecord(AuditRecord auditRecord);


        void AddAuditRecord(string fieldName, 
            Implementation.AuditTransaction.AuditTransactionEventType eventType, 
            Implementation.AuditTransaction.AuditTransactionSubject subjectType, 
            string oldValue, string newValue,
            int? wssAccountId, string userId, string description);
    }
}
