namespace Vinculum.Framework.ErrorHandling
{
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
    using System;
    using System.Runtime.InteropServices;

    public class ExceptionManager
    {
        public static bool ConsumeException(Exception ex, string policy)
        {
            return ExceptionPolicy.HandleException(ex, policy);
        }

        public static bool ConsumeException(Exception ex, string policy, out Exception ox)
        {
            return ExceptionPolicy.HandleException(ex, policy, out ox);
        }
    }
}

