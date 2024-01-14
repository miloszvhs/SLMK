using NHibernate;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;

namespace Core.Upgrades;

public class Upgrade2 : ISqlExecutor
{
    public int Number => 2;

    public DatabaseConnectionKeys ConnectionKey => DatabaseConnectionKeys.Core;

    public void Execute(ISession session)
    {
        CreateAirlinesTable(session);
        CreateAirplanesTable(session);
        CreateFlightsTable(session);
        CreateSeatsTable(session);
        CreateFlightDetailsTable(session);
        CreateReservationsTable(session);
        CreatePaymentsTable(session);
        CreateTicketsTable(session);
    }
    
    private void CreateAirlinesTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Airlines (
            Id SERIAL PRIMARY KEY,
            AirlineName TEXT NOT NULL,
            ContactNumber TEXT,
            ContactEmail TEXT
        );";
        
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
    
    private void CreateAirplanesTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Airplanes (
            Id SERIAL PRIMARY KEY,
            AirlineID INT REFERENCES Airlines(Id),
            Model TEXT NOT NULL,
            ManufactureDate DATE,
            Capacity INT NOT NULL
        );";
        
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
    
    private void CreateFlightsTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Flights (
            Id SERIAL PRIMARY KEY,
            Departure TEXT,
            Destination TEXT,
            FlightDate TIMESTAMP NOT NULL,
            Price DECIMAL(10, 2) NOT NULL,
            AvailableSeats INT NOT NULL
        );";
        
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();    
    }
    
    private void CreateSeatsTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Seats (
            Id SERIAL PRIMARY KEY,
            FlightID INT REFERENCES Flights(Id),
            SeatNumber TEXT NOT NULL,
            Class TEXT NOT NULL,
            IsAvailable BOOLEAN DEFAULT TRUE
        );";
        
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
    
    private void CreateFlightDetailsTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS FlightDetails (
            FlightID INT REFERENCES Flights(Id),
            AirplaneID INT REFERENCES Airplanes(Id),
            DepartureTime TIME,
            ArrivalTime TIME,
            PRIMARY KEY (FlightID, AirplaneID)
        );";
        
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
    
    private void CreateReservationsTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Reservations (
            Id SERIAL PRIMARY KEY,
            UserID TEXT REFERENCES aspnet_users(Id),
            FlightID INT REFERENCES Flights(Id),
            ReservationDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            Status TEXT NOT NULL
        );";

        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
    
    private void CreatePaymentsTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Payments (
            Id SERIAL PRIMARY KEY,
            ReservationID INT REFERENCES Reservations(Id),
            Amount DECIMAL(10, 2) NOT NULL,
            PaymentDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            PaymentMethod TEXT NOT NULL
        );";

        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }

    private void CreateTicketsTable(ISession session)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Tickets (
            Id SERIAL PRIMARY KEY,
            ReservationID INT REFERENCES Reservations(Id),
            SeatID INT REFERENCES Seats(Id),
            TicketDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            TicketNumber TEXT NOT NULL UNIQUE
        );";
        
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
}