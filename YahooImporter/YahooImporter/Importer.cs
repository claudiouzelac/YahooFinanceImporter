using System;
using System.Collections.Generic;
using System.IO;
using CoreUtils.Config;
using CoreUtils.Log;
using SqlUtils;

namespace YahooImporter
{
    internal class Importer
    {
        private List<string> _symbolSet;
        private readonly bool _usingDatabase;

        public Importer()
        {
            _usingDatabase = AppConfig.GetBoolOrThrow("UsingDatabase");
            _symbolSet = new List<string>();
            PopulateSymbolSet();
            SendYahooRequest();
        }

        private void PopulateSymbolSet()
        {
            if(_usingDatabase)
            {
                string databaseFile = Path.Combine(Environment.CurrentDirectory, "stocks.db");
                var database = new Stocks(databaseFile);
            }
            
            bool isSubsetting = AppConfig.GetBoolOrThrow("IsSubsetting");
            if (isSubsetting)
            {
                _symbolSet = AppConfig.GetStringListOrThrow("SymbolSet");
                return;
            }

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string saveDir = AppConfig.GetStringOrThrow("DataDirectory");
            EnsureDirectory(saveDir);
            EnsureSectorDump(baseDirectory, saveDir);
            string path = Path.Combine(baseDirectory, "MasterSymbols.txt");
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                AcquireSymbols(fileStream);
            }
        }

        private static void EnsureDirectory(string saveDir)
        {
            if (!Directory.Exists(saveDir))
            {
                string infoMessage = string.Format("Directory:{0} does not exists....Creating", saveDir);
                AppLog.WriteLog(Level.Info, infoMessage);
                Directory.CreateDirectory(saveDir);
            }
        }

        private void AcquireSymbols(Stream fileStream)
        {
            using (var reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    _symbolSet.Add(line);
                }
            }
        }

        private static void EnsureSectorDump(string baseDirectory, string saveDir)
        {
            if (!File.Exists(saveDir + @"\SectorDump.csv"))
            {
                File.Copy(baseDirectory + @"\SectorDump.csv", saveDir + @"\SectorDump.csv");
            }
        }

        private void SendYahooRequest()
        {
            DateTime startDate = AppConfig.GetDateTimeOrThrow("StartDate");
            var yahoo = new YahooFinanceConnection(_symbolSet,startDate);
        }
    }
}