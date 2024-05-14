using DataInserter.Models;
using DataInserter.TaxiTripReceiveUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils.TaxiTripConverters
{
    internal class TaxiTripStoreAndForwardFlagConverter : IItemConverter<TaxiTrip>
    {
        private readonly Dictionary<string, string> _converts = new()
        {
            {"N", "No" },
            {"Y", "Yes" }
        };

        public TaxiTrip ConvertItem(TaxiTrip item)
        {
            var safFlag = item.StoreAndForwardFlag;

            if(_converts.TryGetValue(safFlag, out var convert))
            {
                item.StoreAndForwardFlag = convert;
            }


            return item;
        }
    }
}
