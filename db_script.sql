CREATE DATABASE TripsDb;
GO

USE TripsDb;
GO

CREATE TABLE TaxiTrips (
    tpep_pickup_datetime DATETIME,
    tpep_dropoff_datetime DATETIME,
    passenger_count INT,
    trip_distance DECIMAL(10, 2),
    store_and_fwd_flag VARCHAR(3),
    PULocationID INT,
    DOLocationID INT,
    fare_amount DECIMAL(10, 2),
    tip_amount DECIMAL(10, 2)
);

CREATE INDEX IX_PULocationID ON TaxiTrips (PULocationID, tip_amount);

CREATE INDEX IX_trip_distance ON TaxiTrips (trip_distance DESC);

CREATE INDEX IX_pickup_dropoff_datetime ON TaxiTrips (tpep_pickup_datetime, tpep_dropoff_datetime);

CREATE INDEX IX_PULocationID_Search ON TaxiTrips (PULocationID);