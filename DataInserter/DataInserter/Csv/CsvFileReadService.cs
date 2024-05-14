using DataInserter.Csv.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Csv
{
    internal class CsvFileReadService : ICsvReadService
    {
        public ICsvReader<T> OpenToRead<T>(string path)
        {
            return new CsvFileReader<T>(path);
        }
    }
}
