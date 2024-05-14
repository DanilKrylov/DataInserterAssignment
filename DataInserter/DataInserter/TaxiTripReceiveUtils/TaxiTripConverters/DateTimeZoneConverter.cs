using DataInserter.Models;
using DataInserter.TaxiTripReceiveUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils.TaxiTripConverters
{
    internal class DateTimeZoneConverter : IItemConverter<TaxiTrip>
    {
        public TaxiTrip ConvertItem(TaxiTrip item)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            item.PickupDateTime = TimeZoneInfo.ConvertTimeToUtc(item.PickupDateTime, timezone);
            item.DropoffDateTime = TimeZoneInfo.ConvertTimeToUtc(item.DropoffDateTime, timezone);

            return item;
        }
    }
}
