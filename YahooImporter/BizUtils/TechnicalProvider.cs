using System.IO;

namespace BizUtils
{
    public sealed class TechnicalProvider
    {
        private const string DataDir = @"C:\data";

        public string EpsEstimateCurrentYear { get; private set; }
        public string EpsNextQuarter { get; private set; }
        public string PeRatio { get; private set; }
        public string PegRatio { get; private set; }

        public void GetFundamentalData(string symbol)
        {
            string path = string.Format(@"{0}\{1}.Technical.csv", DataDir, symbol);
            var info = new FileInfo(path);
            if(!info.Exists)
            {
                return;
            }
            using(FileStream stream = info.OpenRead())
            {
                var reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    string[] strings = reader.ReadLine().Split(',');
                    if(strings[0] == "Name")
                    {
                        continue;
                    }
                    EpsEstimateCurrentYear = strings[3];
                    EpsNextQuarter = strings[21];
                    PeRatio = strings[8];
                    PegRatio = strings[18];
                    return;
                }
            }
        }
    }
}
