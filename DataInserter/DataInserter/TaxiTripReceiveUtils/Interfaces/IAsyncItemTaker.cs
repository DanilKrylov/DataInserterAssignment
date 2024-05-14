using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils.Interfaces
{
    internal interface IAsyncItemTakeChecker<T>
    {
        public Task<bool> CanTakeAsync(T item);
    }
}
