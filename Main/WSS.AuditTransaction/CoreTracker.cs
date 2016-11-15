using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WSS.AuditTransaction.Common;
using WSS.AuditTransaction.Models;

namespace WSS.AuditTransaction
{
    public class CoreTracker
    {
        private readonly ITrackerContext _context;

        public CoreTracker(ITrackerContext context)
        {
            _context = context;
        }

        public List<Models.AuditLog> AuditChanges(object userName)
        {
            var lstAuditLog = new List<Models.AuditLog>();
            foreach (
                DbEntityEntry ent in
                    _context.ChangeTracker.Entries()
                        .Where(p => p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                using (var auditer = new LogAuditor(ent))
                {
                    var eventType = GetEventType(ent);

                    Models.AuditLog record = auditer.CreateLogRecord(userName, eventType, _context);
                    lstAuditLog.Add(record);
                }
            }
            return lstAuditLog;
        }

        public IEnumerable<DbEntityEntry> GetAdditions()
        {
            return _context.ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
        }

        public List<Models.AuditLog> AuditAdditions(object userName, IEnumerable<DbEntityEntry> addedEntries)
        {
            var lstAuditLog = new List<Models.AuditLog>();
            // Get all Added entities
            foreach (DbEntityEntry ent in addedEntries)
            {
                using (var auditer = new LogAuditor(ent))
                {
                    Models.AuditLog record = auditer.CreateLogRecord(userName, EventType.Added, _context);
                    lstAuditLog.Add(record);
                }
            }
            return lstAuditLog;
        }

        private EventType GetEventType(DbEntityEntry entry)
        {
            var eventType = entry.State == EntityState.Modified ? EventType.Modified : EventType.Deleted;
            return eventType;
        }
    }
}