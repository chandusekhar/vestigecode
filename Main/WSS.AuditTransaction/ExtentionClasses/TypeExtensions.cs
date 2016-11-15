using System;

namespace WSS.AuditTransaction.ExtentionClasses
{
    public static class TypeExtensions
    {
        public static bool IsNullable<T>(this Type type)
        {
            return Nullable.GetUnderlyingType(type) == typeof(T);
        }

        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static object DefaultValue(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}