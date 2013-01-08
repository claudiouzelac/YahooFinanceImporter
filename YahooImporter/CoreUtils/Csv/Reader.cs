using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CoreUtils.Csv
{
    public sealed class Reader : IDisposable
    {
        private readonly Stream _stream;

        public Reader(Stream stream)
        {
            _stream = stream;
        }

        public DataTable Read()
        {
            IEnumerable<string> headers = GetHeaders();
            var table = InitTable(headers);
            using (var reader = new StreamReader(_stream))
            {
                string line = reader.ReadLine();
                string[] strings = line.Split(',');
                for (int i = 0; i < strings.Length; i++)
                {
                    DataRow row = table.NewRow();
                    row[i] = strings[i];
                }
            }
            return table;
        }

        private IEnumerable<string> GetHeaders()
        {
            var fields = new List<string>();
            using (var reader = new StreamReader(_stream))
            {
                string line = reader.ReadLine();
                fields.AddRange(line.Split(','));
            }
            return fields;
        }

        private static DataTable InitTable(IEnumerable<string> headers)
        {
            var table = new DataTable();
            foreach (string header in headers)
            {
                table.Columns.Add(header);
            }
            return table;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}
