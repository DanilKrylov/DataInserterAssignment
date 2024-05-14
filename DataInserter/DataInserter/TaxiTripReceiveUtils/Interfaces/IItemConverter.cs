using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils.Interfaces
{
    internal interface IItemConverter<T>
    {
        T ConvertItem(T item);
    }
}
