using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Database.Interfaces
{
    internal interface IDbDataSender : IDisposable
    {
        Task<int> InsertDataAsync<T>(string query, IEnumerable<T> data);
    }
}
