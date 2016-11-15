using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WSS.AuditTransaction.Common;
using WSS.AuditTransaction.Comparators;
using WSS.AuditTransaction.Models;

namespace WSS.AuditTransaction.FactoryClasses
{
    public class ChangeLogDetailsAuditor : ILogDetailsAuditor
    {
        protected readonly DbEntityEntry DbEntry;
        private readonly Models.AuditLog _log;

        public ChangeLogDetailsAuditor(DbEntityEntry dbEntry, Models.AuditLog log)
        {
            DbEntry = dbEntry;
            _log = log;
        }

        public IEnumerable<AuditLogDetail> CreateLogDetails()
        {
            Type entityType = DbEntry.Entity.GetType();

            foreach (string propertyName in PropertyNamesOfEntity())
            {
                yield return new Models.AuditLogDetail
                {
                    PropertyName = propertyName,
                    OriginalValue = OriginalValue(propertyName).ToString(),
                    NewValue = CurrentValue(propertyName).ToString(),
                    Log = _log
                };
            }
        }

        protected internal virtual EntityState StateOfEntity()
        {
            return DbEntry.State;
        }

        private IEnumerable<string> PropertyNamesOfEntity()
        {
            var propertyValues = (StateOfEntity() == EntityState.Added)
                ? DbEntry.CurrentValues
                : DbEntry.OriginalValues;
            return propertyValues.PropertyNames;
        }

        protected virtual bool IsValueChanged(string propertyName)
        {
            var prop = DbEntry.Property(propertyName);
            var propertyType = DbEntry.Entity.GetType().GetProperty(propertyName).PropertyType;

            object originalValue = OriginalValue(propertyName);

            Comparator comparator = ComparatorFactory.GetComparator(propertyType);

            var changed = (StateOfEntity() == EntityState.Modified
                && prop.IsModified && !comparator.AreEqual(CurrentValue(propertyName), originalValue));
            return changed;
        }

        protected virtual object OriginalValue(string propertyName)
        {
            return DbEntry.Property(propertyName).OriginalValue;
        }

        protected virtual object CurrentValue(string propertyName)
        {
            var value = DbEntry.Property(propertyName).CurrentValue;
            return value;
        }
    }
}