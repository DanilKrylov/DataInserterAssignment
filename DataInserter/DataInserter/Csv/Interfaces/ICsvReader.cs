using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Csv.Interfaces
{
    internal interface ICsvReader<T> : IDisposable
    {
        bool TryReadItem(out T? item);
        Task<bool> MoveNextAsync();
    }
}
