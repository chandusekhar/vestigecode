namespace Vinculum.Framework.Utility
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public class UtilityManager
    {
        public static string GetDataFromWebConfig(string section, string key)
        {
            string strData;
            try
            {
                string data = null;
                NameValueCollection nameValue = (NameValueCollection) ConfigurationManager.GetSection(section);
                if (nameValue != null)
                {
                    data = nameValue[key];
                }
                if (data == null)
                {
                    data = string.Empty;
                }
                strData = data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strData;
        }

        public static string GetFormattedXml(string xmlParam)
        {
            xmlParam = xmlParam.Remove(0, 0x27);
            return xmlParam;
        }

        public static string GetServerDate()
        {
            return DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
    }
}

