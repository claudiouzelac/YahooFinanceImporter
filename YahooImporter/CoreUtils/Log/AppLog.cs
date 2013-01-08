using System;
using log4net;
using log4net.Config;

namespace CoreUtils.Log
{
    public static class AppLog
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (AppLog));

        static AppLog()
        {
            XmlConfigurator.Configure();
        }

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
