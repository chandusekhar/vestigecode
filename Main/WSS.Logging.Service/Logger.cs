using log4net;
using System;
using System.IO;

namespace WSS.Logging.Service
{
    public class Logger : ILogger
    {
        private ILog _logger;

        static Logger()
        {
            var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
            System.Diagnostics.Debug.WriteLine("Log4net configuration file location:" + log4NetConfigFilePath);
        }

        public Logger(Type logClass)
        {
            _logger = LogManager.GetLogger(logClass);
        }

        public void Fatal(string errorMessage, Exception exception)
        {
            if (_logger.IsFatalEnabled)
                _logger.Fatal(errorMessage, exception);
        }

        public void Fatal(string errorMessage)
        {
            if (_logger.IsFatalEnabled)
                _logger.Fatal(errorMessage);
        }

        public void Fatal(string format, params object[] args)
        {
            if (_logger.IsFatalEnabled)
                _logger.FatalFormat(format, args);
        }

        public void Error(string errorMessage, Exception exception)
        {
            if (_logger.IsErrorEnabled)
                _logger.Error(errorMessage, exception);
        }

        public void Error(string errorMessage)
        {
            if (_logger.IsErrorEnabled)
                _logger.Error(errorMessage);
        }

        public void Error(string format, params object[] args)
        {
            if (_logger.IsErrorEnabled)
                _logger.ErrorFormat(format, args);
        }

        public void Warn(string message)
        {
            if (_logger.IsWarnEnabled)
                _logger.Warn(message);
        }

        public void Warn(string format, params object[] args)
        {
            if (_logger.IsWarnEnabled)
                _logger.WarnFormat(format, args);
        }

        public void Info(string message)
        {
            if (_logger.IsInfoEnabled)
                _logger.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            if (_logger.IsInfoEnabled)
                _logger.InfoFormat(format, args);
        }

        public void Debug(string message)
        {
            if (_logger.IsDebugEnabled)
                _logger.Debug(message);
        }

        public void Debug(string format, params object[] args)
        {
            if (_logger.IsDebugEnabled)
                _logger.DebugFormat(format, args);
        }
    }
}