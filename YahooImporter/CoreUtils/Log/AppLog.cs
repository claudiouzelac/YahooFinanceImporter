using System;
using log4net;
using log4net.Config;

namespace CoreUtils.Log
{
    /*
     *  The objective of this class is to provide a single log sink for all
     *  application produced messages.  If you wish to add new log messages then
     *  simply call AppLog.WriteLog() with the associated Level that the message 
     *  requires.
     */
    public static class AppLog
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (AppLog));

        /// <summary>
        /// This static method aquires the log configuration from the application's
        /// configuration file (App.config). 
        /// </summary>
        static AppLog()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Writes a new log message to the log file.  Relevant log levels are:
        ///     Debug
        ///     Error
        ///     Fatal
        ///     Info
        ///     Warn
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="log"></param>
        public static void WriteLog(Level logLevel, String log)
        {
            if (logLevel.Equals(Level.Debug))
            {
                Logger.Debug(log);
            }
            else if (logLevel.Equals(Level.Error))
            {
                Logger.Error(log);
            }
            else if (logLevel.Equals(Level.Fatal))
            {
                Logger.Fatal(log);
            }
            else if (logLevel.Equals(Level.Info))
            {
                Logger.Info(log);
            }
            else if (logLevel.Equals(Level.Warn))
            {
                Logger.Warn(log);
            }
        }
    }
}
