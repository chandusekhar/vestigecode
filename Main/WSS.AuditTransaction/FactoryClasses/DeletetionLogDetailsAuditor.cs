using System.Data.Entity.Infrastructure;
using WSS.AuditTransaction.Comparators;
using WSS.AuditTransaction.ExtentionClasses;

namespace WSS.AuditTransaction.FactoryClasses
{
    public class DeletetionLogDetailsAuditor : ChangeLogDetailsAuditor
    {
        public DeletetionLogDetailsAuditor(DbEntityEntry dbEntry, Models.AuditLog log)
            : base(dbEntry, log)
        {
        }

        protected override bool IsValueChanged(string propertyName)
        {
            var propertyType = DbEntry.Entity.GetType().GetProperty(propertyName).PropertyType;
            object defaultValue = propertyType.DefaultValue();
            object orginalvalue = OriginalValue(propertyName);

            Comparator comparator = ComparatorFactory.GetComparator(propertyType);

            return !comparator.AreEqual(defaultValue, orginalvalue);
        }

        protected override object CurrentValue(string propertyName)
        {
            return null;
        }
    }
}