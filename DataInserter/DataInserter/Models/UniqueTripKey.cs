using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Models
{
    internal struct UniqueTripKey
    {
        public DateTime PickupDateTime { get; set; }

        public DateTime DropoffDateTime { get; set; }

        public int? PassengerCount { get; set; }

        public UniqueTripKey(TaxiTrip trip)
        {
            PickupDateTime = trip.PickupDateTime;
            DropoffDateTime = trip.DropoffDateTime;
            PassengerCount = trip.PassengerCount;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + PickupDateTime.GetHashCode();
                hash = hash * 23 + DropoffDateTime.GetHashCode();
                hash = hash * 23 + (PassengerCount?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UniqueTripKey))
                return false;

            UniqueTripKey other = (UniqueTripKey)obj;
            return PickupDateTime == other.PickupDateTime &&
                   DropoffDateTime == other.DropoffDateTime &&
                   PassengerCount == other.PassengerCount;
        }
    }
}
