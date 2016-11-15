using System;
using System.Collections.Generic;
using System.Linq;
using WSS.AuditTransaction.Interfaces;
using WSS.Data;

namespace WSS.AuditTransaction.Implementation
{
    public class AuditTransaction : IAuditTransaction
    {
        private readonly IUnitOfWork _wssUnitOfWork;

        public enum AuditTransactionEventType
        {
            CHNGUID = 0,
            UNSUB =  1,
            ACCTLOCK = 2,
            RSNDACT = 3,
            REG = 4,
            RSTPWD = 5,
            LNKACCT = 6,
            ULINKACCT = 7,
            CHNGNN = 8,
            ADDEMAIL = 9,
            REMEMAIL = 10,
            CHSECQ = 11,
            RSTPWDEMAIL=12,
            ACTVTN=13,
            ACCTLOCKD=14,
            LGOUT=15,
            OPTOUT=16
        }
        public enum AuditTransactionSubject
        {
            WSS_ACCT = 0,
            WSS_INTBILL = 1
        }

        public AuditTransaction(IUnitOfWork wssUnitOfWork)
        {
            _wssUnitOfWork = wssUnitOfWork;
        }

        public void AddAuditRecord(AuditRecord auditRecord)
        {
            _wssUnitOfWork.WssAuditRecordRepository.Insert(auditRecord);
            _wssUnitOfWork.Save(auditRecord.PerformedBy);
        }

        public void AddAuditRecord(string fieldName, AuditTransactionEventType eventType, AuditTransactionSubject subjectType, string oldValue, string newValue, int? wssAccountId, string userId, string description)
        {
            var auditRecord = new AuditRecord()
            {
                date = DateTime.Now,
                AuditEntityId = wssAccountId,
                PerformedBy = userId,
                Description = description,
                AuditEventWssAccountCode = eventType.ToString(),
                AuditSubjectCode = subjectType.ToString()
            };
            AddAuditRecord(auditRecord);
        }
        public IEnumerable<AuditRecord> GetAuditRecords()
        {
            return _wssUnitOfWork.WssAuditRecordRepository.FindAll();
        }

        public IEnumerable<AuditRecord> GetAuditRecords(int wssAccountId)
        {
            return _wssUnitOfWork.WssAuditRecordRepository.FindAll().Where(x => x.AuditEntityId == wssAccountId);
        }

    }
}

