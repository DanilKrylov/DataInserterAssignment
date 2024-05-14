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
    internal class CsvFileReader<T> : ICsvReader<T>
    {
        private readonly CsvReader _csvReader;
        private bool _disposed = false;

        public CsvFileReader(string filePath)
        {
            var reader = new StreamReader(filePath);
            _csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            if(_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _disposed = true;
            _csvReader.Dispose();
        }

        public bool TryReadItem(out T? item)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            try
            {
                item = _csvReader.GetRecord<T>();
                return true;
            }
            catch
            {
                item = default;
                return false;
            }
        }

        public async Task<bool> MoveNextAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            return await _csvReader.ReadAsync();
        }
    }
}
