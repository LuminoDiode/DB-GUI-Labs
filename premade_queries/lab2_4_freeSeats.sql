-- 4.
-- Определить наличие свободных мест на рейс №870 31 декабря 2000г. 

-- Маршрут 870, дату возьмем из существующих:
-- 2000-08-03T18:08:36+04 (ISO 8601)
SELECT 
    Flights.SoldCount AS SeatsSold, 
    SeatsTotal, 
    (Flights.SoldCount<SeatsTotal) as HaveFreeSeats 
        FROM Flights
            JOIN (SELECT
                Airplanes.SeatsCount AS SeatsTotal, 
                Airplanes.Id as plId FROM Airplanes) as foo
            ON plId = Flights.AirplaneId
            WHERE 
            Flights.RouteId = 870 AND
            DATE(Flights.DepartureTime) = '2000-08-03'

-- Successfully run. Total query runtime: 38 msec.
-- 1 rows affected.
-- seats_sold   seats_total     have_free_seats
--         26           130                true