using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Csv.Interfaces
{
    internal interface ICsvWriter<T> : IDisposable
    {
        Task WriteItemAsync(T item);

        Task WriteHeaderAsync();
    }
}
