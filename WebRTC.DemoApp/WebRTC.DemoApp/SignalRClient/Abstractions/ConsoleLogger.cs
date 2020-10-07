// onotseike@hotmail.comPaula Aliu
using System;
namespace WebRTC.DemoApp.SignalRClient.Abstractions
{
    public class ConsoleLogger : ILogger
    {
        #region Implementations of ILogger

        public LogLevel LogLevel { get; set; }

        public void Debug(string tag, string message)
        {
            if (LogLevel > LogLevel.Debug)
                return;
            LogRecord(tag, message, "DEBUG");
        }

        public void Info(string tag, string message)
        {
            if (LogLevel > LogLevel.Info)
                return;
            LogRecord(tag, message, "INFO");
        }

        public void Error(string tag, string message)
        {
            if (LogLevel > LogLevel.Error)
                return;
            LogRecord(tag, message, "ERROR");
        }

        public void Warning(string tag, string message)
        {
            if (LogLevel > LogLevel.Warning)
                return;
            LogRecord(tag, message, "WARNING");
        }

        public void Error(string tag, string message, Exception ex)
        {
            if (LogLevel > LogLevel.Error)
                return;
            LogRecord(tag, message, "ERROR", ex);
        }

        #endregion

        #region Helper Method(s)

        private void LogRecord(string tag, string message, string logType, Exception exc = null)
        {
            string rec;
            if (exc == null)
                rec = $"{DateTime.UtcNow} ({tag}) {logType} - {message}";
            else
                rec =
                    $"{DateTime.UtcNow} ({tag}) {logType} {message}. EXCEPTION: {exc.Message}. STACK TRACE: {exc.StackTrace ?? ""}.";

            System.Diagnostics.Debug.WriteLine(rec);
        }

        #endregion

    }


    #region LogLevel Enum

    public enum LogLevel
    {
        All = 0,
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }

    #endregion


    #region ILogger Interface

    public interface ILogger
    {
        #region Method(s)

        void Debug(string _tag, string _message);
        void Info(string _tag, string _message);
        void Error(string _tag, string _message);
        void Error(string _tag, string _message, Exception _exception);
        void Warning(string _tag, string _message);

        #endregion

    }
    #endregion
}
