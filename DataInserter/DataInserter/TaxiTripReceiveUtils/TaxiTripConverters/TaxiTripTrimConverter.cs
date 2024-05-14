using DataInserter.Models;
using DataInserter.TaxiTripReceiveUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils.TaxiTripConverters
{
    internal class TaxiTripTrimConverter : IItemConverter<TaxiTrip>
    {
        public TaxiTrip ConvertItem(TaxiTrip item)
        {
            item.StoreAndForwardFlag = item.StoreAndForwardFlag.Trim();
            return item;
        }
    }
}
