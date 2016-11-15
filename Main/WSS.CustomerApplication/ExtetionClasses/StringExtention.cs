using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSS.CustomerApplication.ExtetionClasses
{
    public static class StringExtention
    {
        public static string Left(this String input, int length)
        {
            return (input.Length < length) ? input : input.Substring(0, length);
        }
    }
}