using System;
using CoreUtils.Config;
using CoreUtils.Log;

namespace YahooImporter
{
    internal class ImporterApp
    {
        private static int Main()
        {
            try
            {
                AppConfig.FindAppConfigOrThrow();
            }
            catch (Exception exception)
            {
                AppLog.WriteLog(Level.Fatal, exception.Message);
                return 1;
            }
            try
            {
                var importer = new Importer();
                return 0;
            }
            catch (Exception exception)
            {
                AppLog.WriteLog(Level.Fatal, exception.Message);
                return 1;
            }
        }
    }
}