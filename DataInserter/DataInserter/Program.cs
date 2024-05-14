using DataInserter;
using DataInserter.Csv;
using DataInserter.Database;
using DataInserter.Database.Interfaces;
using DataInserter.Models;
using DataInserter.TaxiTripReceiveUtils;
using DataInserter.TaxiTripReceiveUtils.Interfaces;
using DataInserter.TaxiTripReceiveUtils.TaxiTripConverters;
using System.Data.Common;
using System.Data.SqlClient;

internal class Program
{
    private const string CsvPath = "sample-cab-data.csv";
    private const string DuplicateCsvPath = "duplicate.csv";
    private const string ConnectionString = "Server=DESKTOP-6PKE2O9;Database=TripsDb;Trusted_Connection=True;TrustServerCertificate=True";

    private static async Task Main(string[] args)
    {
        var csvReadService = new CsvFileReadService();
        var csvWriteService = new CsvFileWriteService();

        using var csvWriter = csvWriteService.OpenToWrite<TaxiTrip>(DuplicateCsvPath);
        using var csvReader = csvReadService.OpenToRead<TaxiTrip>(CsvPath);

        var csvRetryPolicy = new RetryPolicy(3);
        var trimConverter = new TaxiTripTrimConverter();
        var safFlagConverter = new TaxiTripStoreAndForwardFlagConverter();
        var dateTimeZoneConverter = new DateTimeZoneConverter();
        var takeChecker = new TaxiTripItemTakeCheckerWithLogs(csvWriter);
        var csvItemsReceiver = new CsvItemsReceiver<TaxiTrip>(
            csvReader,
            csvRetryPolicy,
            takeChecker,
            new IItemConverter<TaxiTrip>[] { trimConverter, safFlagConverter, dateTimeZoneConverter }
        );

        using var connection = new SqlConnection(ConnectionString);

        connection.Open();
        using var dataSender = new DbDataSender(connection);

        try
        {
            await ProcessCsvDataAsync(csvItemsReceiver, dataSender);
        }
        finally
        {
            connection.Close();
        }
    }

    private static async Task ProcessCsvDataAsync(CsvItemsReceiver<TaxiTrip> csvItemsReceiver, IDbDataSender dbDataSender)
    {
        var hasNext = true;
        Task<int>? lastDbTask = null;

        while (hasNext)
        {
            var itemsResult = await csvItemsReceiver.TakeItemsAsync(50);
            hasNext = itemsResult.HasNext;

            if (lastDbTask != null)
            {
                await lastDbTask;
            }

            lastDbTask = InsertDataIntoDatabaseAsync(dbDataSender, itemsResult.Result);
        }

        if (lastDbTask != null)
        {
            await lastDbTask;
        }
    }

    private static async Task<int> InsertDataIntoDatabaseAsync(IDbDataSender dbDataSender, List<TaxiTrip> trips)
    {
        return await dbDataSender.InsertDataAsync("INSERT INTO TaxiTrips (tpep_pickup_datetime, tpep_dropoff_datetime, passenger_count, trip_distance, store_and_fwd_flag, PULocationID, DOLocationID, fare_amount, tip_amount) VALUES (@PickupDateTime, @DropoffDateTime, @PassengerCount, @TripDistance, @StoreAndForwardFlag, @PickupLocationID, @DropoffLocationID, @FareAmount, @TipAmount)", trips);
    }
}