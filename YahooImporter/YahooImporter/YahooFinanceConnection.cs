using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using CoreUtils.Config;
using CoreUtils.Csv;
using CoreUtils.Log;

namespace YahooImporter
{
    public sealed class YahooFinanceConnection
    {
        private const string HistoryUrl =
            "http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g=d&ignore.csv";

        private readonly DateTime _startDate;
        private readonly bool _writeToDatabase;
        private readonly bool _writeToDataDirectory;
        private readonly bool _computeAverages;

        public YahooFinanceConnection(IEnumerable<string> symbolSet, DateTime startDate)
        {
            _startDate = startDate;
            _writeToDatabase = AppConfig.GetBoolOr("IsWrittingToDb", false);
            _writeToDataDirectory = AppConfig.GetBoolOr("IsWrittingToDirectory", true);
            _computeAverages = AppConfig.GetBoolOr("IsAveraging", false);

            foreach (string symbol in symbolSet)
            {
                GetStockHistory(symbol, _startDate);
                RequestTechnicalData(symbol);
            }
        }

        private void RequestTechnicalData(string symbol)
        {
            string baseString = "http://download.finance.yahoo.com/d/quotes.csv?s=";
            baseString = baseString + symbol;
            DecorateConnectionString(symbol, baseString);
        }

        private void DecorateConnectionString(string symbol, string connectionString)
        {
            connectionString = connectionString + "&f=";
            string columns = null;
            foreach (YahooTags tag in YahooTags.TagsByName)
            {
                connectionString = connectionString + tag.Tag;
                if (columns == null)
                {
                    columns = tag.ColumnName;
                    continue;
                }
                columns = columns + "," + tag.ColumnName;
            }
            GetTechnicals(connectionString, symbol);
        }

        private void GetTechnicals(string connectionString, string symbol)
        {
            string response = GetResponse(connectionString);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }
            string[] strings = response.Split(',');
            int receivedColumns = strings.Length;
            int actualColumns = YahooTags.TagsByName.Count;
            if (receivedColumns != actualColumns)
            {
                string errorMessage =
                    string.Format("Error=> expected:{0} actual:{1} columns...Please contact Yahoo! for details",
                                  receivedColumns, actualColumns);
                AppLog.WriteLog(Level.Error,errorMessage);
                return;
            }
            string filePath = EnsureFilePath(symbol);

            if(_writeToDataDirectory)
            {
                WriteFundamentalData(strings, filePath);                
            }
        }

        private static void WriteFundamentalData(IList<string> strings, string filePath)
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        string column = GetColumnHeaders();
                        writer.WriteLine(column);
                        string dataPoint = GetDataBody(strings);
                        writer.WriteLine(dataPoint);
                    }
                }
            }
            catch (Exception exception)
            {
                AppLog.WriteLog(Level.Error, exception.Message);
            }
        }

        private static string GetDataBody(IList<string> strings)
        {
            string dataPoint = strings[0];
            for (int i = 1; i < strings.Count; i++)
            {
                dataPoint = dataPoint + "," + strings[i];
            }
            return dataPoint;
        }

        private static string GetColumnHeaders()
        {
            string column = YahooTags.TagsByName[0].ColumnName;
            for (int i = 1; i < YahooTags.TagsByName.Count; i++)
            {
                column = column + "," + YahooTags.TagsByName[i].ColumnName;
            }
            return column;
        }

        private static string EnsureFilePath(string symbol)
        {
            string saveDir = EnsureSaveDirectory();
            string filePath = Path.Combine(saveDir, symbol + ".Technical.csv");
            DeleteOldFile(filePath);
            return filePath;
        }

        private void GetStockHistory(string symbol, DateTime startDate)
        {
            DateTime now = DateTime.Now;
            if (startDate >= now)
            {
                string errorMessage =
                    string.Format("Error: Requested {0} but it is presently {1}....is this what you intended?",
                                  startDate.ToShortDateString(), now.ToShortDateString());
                AppLog.WriteLog(Level.Error, errorMessage);
                return;
            }
            string infoMessage = string.Format("Requesting stock history for ticker:{0} starting at:{1}", symbol,
                                               startDate.ToShortDateString());
            AppLog.WriteLog(Level.Info,infoMessage);
            string url = string.Format(HistoryUrl,
                                       new object[]
                                           {
                                               symbol, startDate.Month - 1, startDate.Day - 5, startDate.Year,
                                               now.Month - 1,
                                               now.Day, now.Year
                                           });
            const int index = 0;
            var provider = new CultureInfo("en-US", true);
            string response = GetResponse(url);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }
            try
            {
                DataTable table = EnsureDataTable(symbol);
                using (var reader = new StringReader(response))
                {
                    reader.ReadLine();
                    while (reader.Peek() > -1)
                    {
                        string[] strArray = reader.ReadLine().Split(new[] {','});
                        DateTime time2 = DateTime.Parse(strArray[index].Replace("\"", ""), provider);
                        if (time2 < startDate) continue;

                        DataRow dataRow = table.NewRow();
                        dataRow["Date"] = time2;
                        dataRow["Open"] = Convert.ToDouble(strArray[1 + index]);
                        dataRow["High"] = Convert.ToDouble(strArray[2 + index]);
                        dataRow["Low"] = Convert.ToDouble(strArray[3 + index]);
                        dataRow["Close"] = Convert.ToDouble(strArray[4 + index]);
                        dataRow["Volume"] = Convert.ToInt64(strArray[5 + index]);
                        table.Rows.Add(dataRow);
                    }
                }
                if(_writeToDataDirectory)
                {
                    WriteTimeSeriesCsv(table);                    
                }
            }
            catch (Exception exception)
            {
                AppLog.WriteLog(Level.Error, exception.Message);
            }
            return;
        }

        private static string AddColumn(DataTable table, int windowLenth)
        {
            string columnName = string.Format("{0}-MovingAverage",windowLenth);
            table.Columns.Add(columnName);
            return columnName;
        }

        private static void WriteTimeSeriesCsv(DataTable table)
        {
            string saveDir = EnsureSaveDirectory();
            string filePath = Path.Combine(saveDir, table.TableName + ".TimeSeries.csv");
            DeleteOldFile(filePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                TextWriter writer = new StreamWriter(stream);
                CsvWriter.WriteToStream(writer, table, true, false);
                writer.Flush();
                writer.Close();
            }
        }

        private static void DeleteOldFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private static string GetResponse(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (WebException webException)
                {
                    AppLog.WriteLog(Level.Error,webException.Message);
                }
                catch (Exception exception)
                {
                    AppLog.WriteLog(Level.Error, exception.Message);
                }
            }
            return string.Empty;
        }

        private static DataTable EnsureDataTable(string symbol)
        {
            var table = new DataTable(symbol);
            table.Columns.Add("Date");
            table.Columns.Add("Open");
            table.Columns.Add("High");
            table.Columns.Add("Low");
            table.Columns.Add("Close");
            table.Columns.Add("Volume");
            return table;
        }

        private static string EnsureSaveDirectory()
        {
            string saveDir = AppConfig.GetStringOrThrow("DataDirectory");
            bool exists = Directory.Exists(saveDir);
            if (!exists)
            {
                Directory.CreateDirectory(saveDir);
            }
            return saveDir;
        }
    }
}