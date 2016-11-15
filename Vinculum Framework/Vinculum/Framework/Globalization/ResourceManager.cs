namespace Vinculum.Framework.Globalization
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Web;
    using Vinculum.Framework.DataTypes;

    public static class ResourceManager
    {
        private static string GetCachedXMLString(string captionId, XMLMessages xmlMessages)
        {
            string value = string.Empty;
            try
            {
                Hashtable hashtable;
                if (xmlMessages == XMLMessages.Caption)
                {
                    hashtable = (Hashtable) HttpContext.Current.Application["Captions"];
                    value = hashtable[captionId].ToString();
                }
                if (xmlMessages == XMLMessages.Message)
                {
                    hashtable = (Hashtable) HttpContext.Current.Application["Messages"];
                    value = hashtable[captionId].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        private static string GetCachedXMLString(string messageId, XMLMessages xmlMessages, string[] inpMsg)
        {
            string value = string.Empty;
            try
            {
                Hashtable hashtable;
                if (xmlMessages == XMLMessages.Caption)
                {
                    hashtable = (Hashtable) HttpContext.Current.Application["Captions"];
                    value = hashtable[messageId].ToString();
                    if (inpMsg != null)
                    {
                        for (int j = 0; j < inpMsg.Length; j++)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("{");
                            sb.Append(j.ToString());
                            sb.Append("}");
                            value = value.Replace(sb.ToString(), inpMsg[j]);
                            sb = null;
                        }
                    }
                }
                if (xmlMessages != XMLMessages.Message)
                {
                    return value;
                }
                hashtable = (Hashtable) HttpContext.Current.Application["Messages"];
                value = hashtable[messageId].ToString();
                if (inpMsg == null)
                {
                    return value;
                }
                for (int j = 0; j < inpMsg.Length; j++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{");
                    sb.Append(j.ToString());
                    sb.Append("}");
                    value = value.Replace(sb.ToString(), inpMsg[j]);
                    sb = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        public static string GetCaptionString(string captionId)
        {
            return GetCachedXMLString(captionId, XMLMessages.Caption);
        }

        public static string GetCaptionString(string messageId, string[] inpMsg)
        {
            return GetCachedXMLString(messageId, XMLMessages.Message, inpMsg);
        }
    }
}

