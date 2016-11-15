using System.Collections.Generic;
using WSS.AuditTransaction.Models;

namespace WSS.AuditTransaction.Common
{
    public interface ILogDetailsAuditor
    {
        IEnumerable<AuditLogDetail> CreateLogDetails();
    }
}