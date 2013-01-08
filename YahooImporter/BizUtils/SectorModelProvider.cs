using System.Data;
using System.IO;

namespace BizUtils
{
    public sealed class SectorModelProvider
    {
        private const string SectorPath = @"C:\data\SectorDump.csv";

        public DataTable SectorModel { get; private set; }

        public SectorModelProvider()
        {
            LoadFromCsv();
        }

        public string FindSectorBySymbol(string symbol)
        {
            foreach (DataRow row in SectorModel.Rows)
            {
                var symbolName = (string)row["Symbol"];
                if(symbolName != symbol)
                {
                    continue;
                }
                return (string) row["IndustryLevelOneID"];
            }
            return null;
        }

        public DataTable GetRelatedSymbolsByIndustry(string industry)
        {
            var table = SectorModel.Copy();
            /*foreach (DataRow row in SectorModel.Rows)
            {
                var industryOne = (string)row["IndustryLevelOneID"];
                if(string.IsNullOrEmpty(industryOne))
                {
                    table.Rows.Remove(row);
                }
                if (industry != industryOne)
                {
                    table.Rows.Remove(row);
                }
            }*/
            return table;
        }

        private void LoadFromCsv()
        {
            var info = new FileInfo(SectorPath);
            if(!info.Exists)
            {
                return;
            }
            using(FileStream stream = info.OpenRead())
            {
                var reader = new Reader(stream);
                SectorModel = reader.Read();
            }
        }
    }
}
