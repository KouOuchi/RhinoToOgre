using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoToOgre
{
    public enum LogLevel
    {
        Fatal = 1,
        Error,
        Warn,
        Info,
        Debug
    }

    public static class RhinoLogger
    {
        public static void Debug(object message)
        {
            Print(LogLevel.Debug, message);
        }
        public static void Debug(object message, Exception exception)
        {
            Print(LogLevel.Debug, message, exception);
        }
        public static void DebugFormat(string format, params object[] args)
        {
            Print(LogLevel.Debug, format, args);
        }
        public static void DebugFormat(string format, object arg0)
        {
            Print(LogLevel.Debug, format, arg0);
        }
        public static void DebugFormat(string format, object arg0, object arg1)
        {
            Print(LogLevel.Debug, format, arg0, arg1);
        }
        public static void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            Print(LogLevel.Debug, format, arg0, arg1, arg2);
        }
        public static void Error(object message)
        {
            Print(LogLevel.Error, message);
        }
        public static void Error(object message, Exception exception)
        {
            Print(LogLevel.Error, message, exception);
        }
        public static void ErrorFormat(string format, params object[] args)
        {
            Print(LogLevel.Error, format, args);
        }
        public static void ErrorFormat(string format, object arg0)
        {
            Print(LogLevel.Error, format, arg0);
        }
        public static void ErrorFormat(string format, object arg0, object arg1)
        {
            Print(LogLevel.Error, format, arg0, arg1);
        }
        public static void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            Print(LogLevel.Error, format, arg0, arg1);
        }
        public static void Fatal(object message)
        {
            Print(LogLevel.Fatal, message);
        }
        public static void Fatal(object message, Exception exception)
        {
            Print(LogLevel.Fatal, message, exception);
        }
        public static void FatalFormat(string format, object arg0)
        {
            Print(LogLevel.Fatal, format, arg0);
        }
        public static void FatalFormat(string format, params object[] args)
        {
            Print(LogLevel.Fatal, format, args);
        }
        public static void FatalFormat(string format, object arg0, object arg1)
        {
            Print(LogLevel.Fatal, format, arg0, arg1);
        }
        public static void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            Print(LogLevel.Fatal, format, arg0, arg1, arg2);
        }
        public static void Info(object message)
        {
            Print(LogLevel.Info, message);
        }
        public static void Info(object message, Exception exception)
        {
            Print(LogLevel.Info, message, exception);
        }
        public static void InfoFormat(string format, object arg0)
        {
            Print(LogLevel.Info, format, arg0);
        }
        public static void InfoFormat(string format, params object[] args)
        {
            Print(LogLevel.Info, format, args);
        }
        public static void InfoFormat(string format, object arg0, object arg1)
        {
            Print(LogLevel.Info, format, arg0, arg1);
        }
        public static void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            Print(LogLevel.Info, format, arg0, arg1, arg2);
        }
        public static void Warn(object message)
        {
            Print(LogLevel.Warn, message);
        }
        public static void Warn(object message, Exception exception)
        {
            Print(LogLevel.Warn, message, exception);
        }
        public static void WarnFormat(string format, object arg0)
        {
            Print(LogLevel.Warn, format, arg0);
        }
        public static void WarnFormat(string format, params object[] args)
        {
            Print(LogLevel.Warn, format, args);
        }
        public static void WarnFormat(string format, object arg0, object arg1)
        {
            Print(LogLevel.Warn, format, arg0, arg1);
        }
        public static void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            Print(LogLevel.Warn, format, arg0, arg1, arg2);
        }
        private static LogLevel _currentLogLevel = LogLevel.Debug;
        public static LogLevel CurrentLogLevel
        {
            get { return _currentLogLevel; }
            set { _currentLogLevel = value; }
        }

        private static void Print(LogLevel logLevel, object message)
        {
            RhinoApp.WriteLine("[{0}]: {1}", Enum.GetName(typeof(LogLevel), logLevel), message);
        }
        private static void Print(LogLevel logLevel, object message, Exception exception)
        {
            Print(logLevel, string.Format("{0} {1}", message, exception.StackTrace) );
        }
        private static void Print(LogLevel logLevel, string format, object arg0)
        {
            Print(logLevel, string.Format(format, arg0));
        }
        private static void Print(LogLevel logLevel, string format, params object[] args)
        {
            RhinoApp.WriteLine("[{0}]: {1}", Enum.GetName(typeof(LogLevel), logLevel), string.Format(format, args));
        }
        private static void Print(LogLevel logLevel, string format, object arg0, object arg1)
        {
            Print(logLevel, string.Format(format, arg0, arg1));
        }
        private static void Print(LogLevel logLevel, string format, object arg0, object arg1, object arg2)
        {
            Print(logLevel, string.Format(format, arg0, arg1, arg2));
        }

        private static bool IsBiggerThanCurrentLogLevel(LogLevel logLevel)
        {
            return logLevel >= CurrentLogLevel;
        }
    }
}
