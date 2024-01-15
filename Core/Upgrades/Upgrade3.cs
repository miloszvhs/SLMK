using NHibernate;
using ORM.Contract.Enums;
using ORM.Contract.Interfaces;

namespace Core.Upgrades;

public class Upgrade3 : ISqlExecutor
{
    public int Number => 3;

    public DatabaseConnectionKeys ConnectionKey => DatabaseConnectionKeys.Core;

    public void Execute(ISession session)
    {
        SeedFlights(session);
    }

    private void SeedFlights(ISession session)
    {
        var sql = @"
        INSERT INTO Flights (Id, Departure, Destination, FlightDate, Price, AvailableSeats)
        VALUES (1, 'Sofia', 'London', '2021-01-01 12:00:00', 90.00, 20),
               (2, 'London', 'Warsaw', '2021-01-02 12:00:00', 100.00, 20),
               (3, 'Sofia', 'Berlin', '2021-01-03 12:00:00', 100.00, 20),
               (4, 'Praga', 'Poznan', '2021-01-04 12:00:00', 120.00, 30),
                (5, 'Sofia', 'London', '2021-01-05 12:00:00', 100.00, 20),
                (6, 'London', 'Warsaw', '2021-01-06 12:00:00', 100.00, 20),
                (7, 'Sofia', 'Berlin', '2021-01-07 12:00:00', 100.00, 30),
                (8, 'Praga', 'Poznan', '2021-01-08 12:00:00', 120.00, 30),
                (9, 'Sofia', 'London', '2021-01-09 12:00:00', 130.00, 20),
                (10, 'London', 'Warsaw', '2021-01-10 12:00:00', 80.00, 30);
        ";
            
        session
            .CreateSQLQuery(sql)
            .ExecuteUpdate();
    }
}