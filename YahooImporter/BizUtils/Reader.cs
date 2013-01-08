using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BizUtils
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
            var headers = (List<string>)GetHeaders();
            var table = InitTable(headers);
            using (var reader = new StreamReader(_stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] strings = line.Split(',');
                    if(strings.Length < headers.Count)
                    {
                        continue;
                    }
                    DataRow row = table.NewRow();
                    row[0] = false;
                    for (int i = 0; i < headers.Count; i++)
                    {
                        row[i+1] = strings[i];
                    }
                    table.Rows.Add(row);    
                }
            }
            return table;
        }

        private IEnumerable<string> GetHeaders()
        {
            var fields = new List<string>();
            var reader = new StreamReader(_stream);
            string line = reader.ReadLine();
            fields.AddRange(line.Split(','));
            return fields;
        }

        private static DataTable InitTable(IEnumerable<string> headers)
        {
            var table = new DataTable();
            table.Columns.Add("Include", typeof (bool));
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
