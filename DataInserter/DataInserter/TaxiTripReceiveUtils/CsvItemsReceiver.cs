using CsvHelper;
using DataInserter.Csv;
using DataInserter.Csv.Interfaces;
using DataInserter.Models;
using DataInserter.TaxiTripReceiveUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.TaxiTripReceiveUtils
{
    internal class CsvItemsReceiver<T>
    {
        private readonly ICsvReader<T> _csvReader;
        private readonly IAsyncItemTakeChecker<T> _itemTakeChecker;
        private readonly IEnumerable<IItemConverter<T>> _itemConverters;
        private readonly RetryPolicy _retryPolicy;
        private bool isFirstReceived = false;

        public CsvItemsReceiver(ICsvReader<T> csvReader, RetryPolicy retryPolicy, IAsyncItemTakeChecker<T> itemTakeChecker, IEnumerable<IItemConverter<T>> itemConverters)
        {
            _csvReader = csvReader;
            _retryPolicy = retryPolicy;
            _itemTakeChecker = itemTakeChecker;
            _itemConverters = itemConverters;
        }

        public async Task<TakeItemsResult<T>> TakeItemsAsync(int count)
        {
            if (!isFirstReceived)
            {
                isFirstReceived = true;
                await _csvReader.MoveNextAsync();
            }
            var hasNext = true;
            var items = new List<T>(count);

            while (hasNext && items.Count < count)
            {
                if (_csvReader.TryReadItem(out var item))
                {
                    hasNext = await _csvReader.MoveNextAsync();

                    if (!await _itemTakeChecker.CanTakeAsync(item))
                    {
                        continue;
                    }

                    foreach(var itemConverter in _itemConverters)
                    {
                        item = itemConverter.ConvertItem(item);
                    }

                    items.Add(item);
                }
                else
                {
                    _retryPolicy.OnFailTry();
                }
            }

            return new TakeItemsResult<T>(items, hasNext);
        }
    }
}
