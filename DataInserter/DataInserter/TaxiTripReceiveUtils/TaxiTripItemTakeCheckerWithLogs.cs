using CsvHelper;
using DataInserter.Csv.Interfaces;
using DataInserter.Models;
using DataInserter.TaxiTripReceiveUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils
{
    internal class TaxiTripItemTakeCheckerWithLogs : IAsyncItemTakeChecker<TaxiTrip>
    {
        private readonly ICsvWriter<TaxiTrip> _duplicatesWriter;
        private readonly HashSet<UniqueTripKey> _uniqueItemKeys = new();

        public TaxiTripItemTakeCheckerWithLogs(ICsvWriter<TaxiTrip> duplicatesWriter)
        {
            _duplicatesWriter = duplicatesWriter;
        }

        public async Task<bool> CanTakeAsync(TaxiTrip item)
        {
            var key = new UniqueTripKey(item);

            if (_uniqueItemKeys.Contains(key))
            {
                await _duplicatesWriter.WriteItemAsync(item);
                return false;
            }

            _uniqueItemKeys.Add(key);
            return true;
        }
    }
}
