using CsvHelper;
using DataInserter.Csv.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Csv
{
    internal class CsvFileWriter<T> : ICsvWriter<T>
    {
        private readonly CsvWriter _csvWriter;
        private bool _disposed = false;

        public CsvFileWriter(string filePath)
        {
            var fileStream = new StreamWriter(filePath);
            _csvWriter = new CsvWriter(fileStream, CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _disposed = true;
            _csvWriter.Dispose();
        }
        public async Task WriteHeaderAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _csvWriter.WriteHeader<T>();
            await _csvWriter.NextRecordAsync();
        }

        public async Task WriteItemAsync(T item)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _csvWriter.WriteRecord(item);
            await _csvWriter.NextRecordAsync();
        }
    }
}
