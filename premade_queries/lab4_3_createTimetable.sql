-- 3. 
-- Создать таблицу расписания самолетов по 
-- маршруту «Москва»-«Васюки».

-- Пусть '«Москва»' - '«Васюки»'   
-- == 'Padbergport' - 'Hilllshire'

SELECT 
    Routes.DeparturePoint, 
    Routes.DestinationPoint,
	depT as DepartureTime,
	arrT as ArrivalTime
    FROM Routes
    JOIN (SELECT 
        Flights.RouteId as flId,
        Flights.DepartureTime as depT,
        Flights.ArrivalTime as arrT
        FROM Flights) as foo
    ON Routes.Id = flId
    WHERE 
        Routes.DeparturePoint = 'Padbergport' AND
        Routes.DestinationPoint = 'Hilllshire'
    ORDER BY
        depT;

-- Successfully run. Total query runtime: 38 msec.
-- 29 rows affected.