using System;

namespace WSS.Logging.Service
{
    public interface ILogger
    {
        void Fatal(string errorMessage, Exception exception);

        void Fatal(string errorMessage);

        void Fatal(string format, params object[] args);

        void Error(string errorMessage, Exception exception);

        void Error(string errorMessage);

        void Error(string format, params object[] args);

        void Warn(string message);

        void Warn(string format, params object[] args);

        void Info(string message);

        void Info(string format, params object[] args);

        void Debug(string message);

        void Debug(string format, params object[] args);
    }
}