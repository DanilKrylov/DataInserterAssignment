using DataInserter.Csv.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Csv
{
    internal class CsvFileWriteService : ICsvWriteService
    {
        public ICsvWriter<T> OpenToWrite<T>(string path)
        {
            return new CsvFileWriter<T>(path);
        }
    }
}
