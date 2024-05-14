using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Models
{
    public struct TaxiTrip
    {
        [Name("tpep_pickup_datetime")]
        public DateTime PickupDateTime { get; set; }

        [Name("tpep_dropoff_datetime")]
        public DateTime DropoffDateTime { get; set; }

        [Name("passenger_count")]
        public int? PassengerCount { get; set; }

        [Name("trip_distance")]
        public decimal TripDistance { get; set; }

        [Name("store_and_fwd_flag")]
        public string StoreAndForwardFlag { get; set; }

        [Name("PULocationID")]
        public int PickupLocationID { get; set; }

        [Name("DOLocationID")]
        public int DropoffLocationID { get; set; }

        [Name("fare_amount")]
        public decimal FareAmount { get; set; }

        [Name("tip_amount")]
        public decimal TipAmount { get; set; }
    }
}
