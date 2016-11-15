namespace Vinculum.Framework.Logging
{
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using System.Threading;

    public static class LogManager
    {
        public static void WriteExceptionLog(Exception exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            LogEntry logEntry = new LogEntry();
            try
            {
                if (Logger.IsLoggingEnabled())
                {
                    if ((exception.StackTrace != null) && (exception.TargetSite != null))
                    {
                        stringBuilder.Append(Environment.NewLine + "Page Name: " + exception.StackTrace.Substring(exception.StackTrace.LastIndexOf(@"\") + 1).ToString() + Environment.NewLine);
                        stringBuilder.Append("Namespace: " + exception.TargetSite.DeclaringType.FullName.ToString() + Environment.NewLine);
                        stringBuilder.Append("Method Name: " + exception.TargetSite.Name.ToString() + Environment.NewLine);
                        stringBuilder.Append("Error Description: " + exception.Message.ToString());
                    }
                    else
                    {
                        stringBuilder.Append(exception.Message.ToString());
                    }
                    logEntry.Message = stringBuilder.ToString();
                    logEntry.Title = exception.Source;
                    CultureInfo culture = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
                    logEntry.TimeStamp = Convert.ToDateTime(DateTime.Now, culture);
                    logEntry.Categories.Add("Exception");
                    logEntry.Priority = 2;
                    logEntry.Severity = TraceEventType.Error;
                    logEntry.ProcessName = exception.StackTrace;
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    if (exception.Data.Count > 0)
                    {
                        IDictionaryEnumerator iDictionaryEnumerator = exception.Data.GetEnumerator();
                        while (iDictionaryEnumerator.MoveNext())
                        {
                            string strKey = iDictionaryEnumerator.Entry.Key.ToString();
                            string strValue = iDictionaryEnumerator.Entry.Value.ToString();
                            dictionary.Add(strKey, strValue);
                        }
                    }
                    logEntry.ExtendedProperties = dictionary;
                    Logger.Write(logEntry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                logEntry = null;
            }
        }

        public static void WriteInformationLog(string message)
        {
            LogEntry logEntry = new LogEntry();
            try
            {
                if (message != null)
                {
                    logEntry.Message = message.ToString();
                }
                logEntry.Message = message;
                CultureInfo culture = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
                logEntry.TimeStamp = Convert.ToDateTime(DateTime.Now, culture);
                logEntry.Categories.Add("Tracing");
                logEntry.Priority = -2;
                logEntry.Severity = TraceEventType.Information;
                Logger.Write(logEntry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                logEntry = null;
            }
        }
    }
}

